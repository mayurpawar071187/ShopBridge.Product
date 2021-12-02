using ShopBridge.Product.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Product.Business
{
    public interface IItemRepository
    {
        Task<string> Add(Item item);
        Task<bool> update(string id,Item item);
        Task<bool> delete(string id);
        Task<Item> get(string id);
        Task<List<Item>> getlist(ItemSearch itemsearch);
    }
}
