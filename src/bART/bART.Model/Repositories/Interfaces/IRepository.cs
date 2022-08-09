using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Model.Repositories.Interfaces
{
    public interface IRepository<TItem, in TKey>
    {
        Task AddItem(TItem item);
        Task<TItem> GetItemByKey(TKey key);
        Task<ICollection<TItem>> GetItems();
        Task EditItem(TItem item);
        Task DeleteItem(TKey key);
        Task AddItems(List<TItem> items);
    }
}
