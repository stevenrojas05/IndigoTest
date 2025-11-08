using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleItemController : ControllerBase
    {
        private readonly ISaleItemService _saleItemService;

        public SaleItemController(ISaleItemService saleItemService)
        {
            _saleItemService = saleItemService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaleItem>> GetById(int id)
        {
            var product = await _saleItemService.GetBySaleIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<SaleItem>> Create(SaleItem saleItem)
        {
            var created = await _saleItemService.CreateAsync(saleItem);
            return CreatedAtAction(nameof(GetById), new { id = created.ProductId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SaleItem saleItem)
        {
            if (id != saleItem.SaleItemId)
                return BadRequest();

            await _saleItemService.UpdateAsync(saleItem);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _saleItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}
