using ShopBridge.Product.DataBase.DBEntity;
using ShopBridge.Product.Model.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopBridge.Product.Business.Repository
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        public readonly shopbridgeContext _shopbridgeContext;
        public ItemCategoryRepository(shopbridgeContext shopbridgeContext)
        {
            _shopbridgeContext = shopbridgeContext;
        }

        public async Task<string> Add(ItemCategory item)
        {
            var data = new Itemcategory()
            {
                Id = Guid.NewGuid().ToString(),
                Code = item.Code,
                Createdby = "",//Pass User Hear
                Createdon = DateTime.Now,
                Description = item.Description,
                Isactive = item.Isactive,
                Categoryname = item.Categoryname,
                Modifiedby = "",
                Modifiedon = null
            };
            _shopbridgeContext.Itemcategories.Add(data);

            await _shopbridgeContext.SaveChangesAsync();

            return data.Id;
        }

        public async Task<bool> delete(string id)
        {
            var deleteitem= await _shopbridgeContext.Itemcategories.Where(x=>x.Id== id).FirstOrDefaultAsync();
            _shopbridgeContext.Itemcategories.Remove(deleteitem);

            await _shopbridgeContext.SaveChangesAsync();
            return true;
        }

        public async Task<ItemCategory> get(string id)
        {
            var data = await _shopbridgeContext.Itemcategories.Where(x=>x.Id==id).Select(x => new ItemCategory()
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description,
                Isactive = x.Isactive,
                //ItemImagePath=
                Categoryname = x.Categoryname
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<ItemCategory>> getlist()
        {

            var data = await _shopbridgeContext.Itemcategories.Select(x => new ItemCategory()
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description,
                Isactive = x.Isactive,
                //ItemImagePath=
                Categoryname = x.Categoryname
            }).ToListAsync();

            return data;
        }
        public async Task<bool> update(string id, ItemCategory item)
        {
            var data = new Itemcategory()
            {
                Id = id,
                Code = item.Code,
                Createdby = "",//Pass User Hear
                Createdon = DateTime.Now,
                Description = item.Description,
                Isactive = item.Isactive,
                Categoryname = item.Categoryname,
                Modifiedby = "",
                Modifiedon = null
            };
            _shopbridgeContext.Itemcategories.Update(data);

            await _shopbridgeContext.SaveChangesAsync();
            return true;
        }
    }
}
