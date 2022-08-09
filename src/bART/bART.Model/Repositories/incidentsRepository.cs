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
    public class incidentsRepository : IRepository<incidents, string>
    {
        private readonly IServiceScopeFactory _factory;

        public incidentsRepository(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public async Task AddItem(incidents item)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.incidents.Add(item);
            await db.SaveChangesAsync();
        }
        public async Task AddItems(List<incidents> items)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            foreach (var item in items)
            {
            db.incidents.Add(item);
            }
            await db.SaveChangesAsync();
        }

        public async Task DeleteItem(string key)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Remove(new incidents { name = key });
            await db.SaveChangesAsync();
        }

        public async Task EditItem(incidents item)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Update(item);
            await db.SaveChangesAsync();
        }

        public async Task<incidents> GetItemByKey(string key)
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            return await db.incidents.FirstOrDefaultAsync(u => u.name == key);
        }

        public async Task<ICollection<incidents>> GetItems()
        {
            using var scope = _factory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            return await db.incidents.ToListAsync();
        }
    }
}
