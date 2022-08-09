using bART.Model.Entities;
using bART.Model.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Model.Repositories
{
    public class contactsRepository : IRepository<contacts, string>
    {
        private readonly IServiceScopeFactory _factory;

        public contactsRepository(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public async Task AddItem(contacts item)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.contacts.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task AddItems(List<contacts> items)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            foreach (var item in items)
            {
            db.contacts.Add(item);
            }
            await db.SaveChangesAsync();
        }

        public async Task DeleteItem(string key)
        {

            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Remove(new contacts { email = key });
            await db.SaveChangesAsync();
        }

        public async Task EditItem(contacts item)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Update(item);
            await db.SaveChangesAsync();
        }

        public async Task<contacts> GetItemByKey(string key)
        {

            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            return await db.contacts.FirstOrDefaultAsync(u => u.email == key);
        }

        public async Task<ICollection<contacts>> GetItems()
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            return await db.contacts.ToListAsync();
        }
    }
}
