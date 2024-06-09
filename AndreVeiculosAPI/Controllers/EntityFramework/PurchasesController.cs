using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public PurchasesController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchase()
        {
            if (_context.Purchase == null)
            {
                return NotFound();
            }
            return await _context.Purchase.Include(x => x.Car).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
            if (_context.Purchase == null)
            {
                return NotFound();
            }
            var purchase = await _context.Purchase.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(PurchaseDTO dto)
        {
            Purchase purchase = new(dto);
            purchase.Car = await _context.Car.FindAsync(dto.CarPlate);

            if (_context.Purchase == null)
            {
                return Problem("Entity set 'AndreVeiculosAPIContext.Purchase'  is null.");
            }
            _context.Purchase.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
        }

        private bool PurchaseExists(int id)
        {
            return (_context.Purchase?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
