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
    public class ItemController : ControllerBase
    {
        public readonly IItemRepository _itemRepository;
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("getall")]
        public async Task<IActionResult> GetAllItem([FromBody] ItemSearch itemsearch)
        {
            var data = await _itemRepository.getlist(itemsearch);
            return Ok(data);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            var data = await _itemRepository.get(id);
            if (data == null)
                return NotFound();
            else
                return Ok(data);
        }

        [HttpPost("")]
        
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            var data = await _itemRepository.Add(item);
            return CreatedAtAction(nameof(GetItem), new { id = data, controller = "Item" }, data); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string id, [FromBody] Item item)
        {
            var data = await _itemRepository.update(id, item);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id)
        {
            var data = await _itemRepository.delete(id);
            return Ok(data);
        }
    }
}
