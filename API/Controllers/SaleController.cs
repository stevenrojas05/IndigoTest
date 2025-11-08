using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
        {
            var sale = await _saleService.GetAllAsync();
            return Ok(sale);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Create(Sale sale)
        {
            var created = await _saleService.CreateAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = created.SaleId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Sale sale)
        {
            if (id != sale.SaleId)
                return BadRequest();

            await _saleService.UpdateAsync(sale);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _saleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
