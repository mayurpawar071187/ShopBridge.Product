using ShopBridge.Product.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Product.Business
{
    public interface IItemCategoryRepository
    {
        Task<string> Add(ItemCategory item);
        Task<bool> update(string id, ItemCategory item);
        Task<bool> delete(string id);
        Task<ItemCategory> get(string id);
        Task<List<ItemCategory>> getlist();
    }
}
