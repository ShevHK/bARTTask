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
    public class accountsRepository : IRepository<accounts, string>
    {
        private readonly IServiceScopeFactory _factory;

        public accountsRepository(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public async Task AddItem(accounts item)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.accounts.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task AddItems(List<accounts> items)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            foreach (var item in items)
            {
            db.accounts.Add(item);
            }
            await db.SaveChangesAsync();
        }

        public async Task DeleteItem(string key)
        {

            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Remove(new accounts { name = key });
            await db.SaveChangesAsync();
        }

        public async Task EditItem(accounts item)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Update(item);
            await db.SaveChangesAsync();
        }

        public async Task<accounts> GetItemByKey(string key)
        {

            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            return await db.accounts.FirstOrDefaultAsync(u => u.name == key);
        }

        public async Task<ICollection<accounts>> GetItems()
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            return await db.accounts.ToListAsync();
        }
    }
}
