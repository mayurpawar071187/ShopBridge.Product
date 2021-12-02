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
    public class ItemRepository : IItemRepository
    {
        public readonly shopbridgeContext _shopbridgeContext;
        public ItemRepository(shopbridgeContext shopbridgeContext)
        {
            _shopbridgeContext = shopbridgeContext;
        }

        public async Task<string> Add(Item item)
        {
            var data = new Itemmaster()
            {
                Id = Guid.NewGuid().ToString(),
                Categoryid = item.Categoryid,
                Code = item.Code,
                Createdby = "",//Pass User Hear
                Createdon = DateTime.Now,
                Description = item.Description,
                Isactive = item.Isactive,
                Itemname = item.Itemname,
                Modifiedby = "",
                Modifiedon = null
            };
            _shopbridgeContext.Itemmasters.Add(data);

            await _shopbridgeContext.SaveChangesAsync();

            return data.Id;
        }

        public async Task<bool> delete(string id)
        {
            var deleteitem = await _shopbridgeContext.Itemmasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            _shopbridgeContext.Itemmasters.Remove(deleteitem);

            await _shopbridgeContext.SaveChangesAsync();
            return true;
        }

        public async Task<Item> get(string id)
        {
            var data = await _shopbridgeContext.Itemmasters.Where(x=>x.Id==id).Select(x => new Item()
            {
                Id = x.Id,
                Categoryid = x.Categoryid,
                Code = x.Code,
                Description = x.Description,
                Isactive = x.Isactive,
                //ItemImagePath=
                Itemname = x.Itemname
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<Item>> getlist(ItemSearch itemsearch)
        {
            var queryResultPage = new List<Item>();

            var data = await _shopbridgeContext.Itemmasters.Where(x => x.Itemname.Contains(itemsearch.Itemname)
            || x.Code.Contains(itemsearch.Code)
            || x.Description.Contains(itemsearch.Description)).Select(x => new Item()
            {
                Id = x.Id,
                Categoryid = x.Categoryid,
                Code = x.Code,
                Description = x.Description,
                Isactive = x.Isactive,
                //ItemImagePath=
                Itemname = x.Itemname
            }).ToListAsync();

            if (itemsearch.page > 0 && itemsearch.size > 0)
            {
                queryResultPage = data.Skip((itemsearch.page - 1) * itemsearch.size).Take(itemsearch.size).ToList();
            }
            else
            { queryResultPage = data; }

            return queryResultPage;
        }
        public async Task<bool> update(string id, Item item)
        {
            var data = new Itemmaster()
            {
                Id = id,
                Categoryid = item.Categoryid,
                Code = item.Code,
                Createdby = "",//Pass User Hear
                Createdon = DateTime.Now,
                Description = item.Description,
                Isactive = item.Isactive,
                Itemname = item.Itemname,
                Modifiedby = "",
                Modifiedon = null
            };
            _shopbridgeContext.Itemmasters.Update(data);

            await _shopbridgeContext.SaveChangesAsync();
            return true;
        }
    }
}
