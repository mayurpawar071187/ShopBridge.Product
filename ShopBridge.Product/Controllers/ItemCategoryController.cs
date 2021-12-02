using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Product.Business;
using ShopBridge.Product.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class ItemCategoryController : ControllerBase
    {
        public readonly IItemCategoryRepository _itemCategoryRepository;
        public ItemCategoryController(IItemCategoryRepository itemCategoryRepository)
        {
            _itemCategoryRepository = itemCategoryRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetAllItem()
        {
            var data = await _itemCategoryRepository.getlist();
            return Ok(data);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            var data = await _itemCategoryRepository.get(id);
            if (data == null)
                return NotFound();
            else
                return Ok(data);
        }

        [HttpPost("")]
        
        public async Task<IActionResult> AddItem([FromBody] ItemCategory item)
        {
            var data = await _itemCategoryRepository.Add(item);
            return CreatedAtAction(nameof(GetItem), new { id = data, controller = "Item" }, data); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string id, [FromBody] ItemCategory item)
        {
            var data = await _itemCategoryRepository.update(id, item);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id)
        {
            var data = await _itemCategoryRepository.delete(id);
            return Ok(data);
        }
    }
}
