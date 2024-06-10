using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public SalesController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSale()
        {
            if (_context.Sale == null)
            {
                return NotFound();
            }
            return await _context.Sale.Include(x => x.Car).Include(x => x.Customer).Include(x => x.Customer.Address).Include(x => x.Employee).Include(x => x.Employee.Address).Include(x => x.Employee.Function).Include(x => x.Payment).Include(x => x.Payment.Fetlock).Include(x => x.Payment.Card).Include(x => x.Payment.Pix).Include(x => x.Payment.Pix.Type).ToListAsync();
        }

        [HttpGet("entity{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            if (_context.Sale == null)
            {
                return NotFound();
            }
            var sale = await _context.Sale.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        [HttpPost("entity")]
        public async Task<ActionResult<Sale>> PostSale(SaleDTO dto)
        {
            Sale sale = new(dto);

            sale.Car = await _context.Car.FindAsync(dto.CarPlate);
            sale.Customer = await _context.Customer.FindAsync(dto.CustomerDocument);
            sale.Employee = await _context.Employee.FindAsync(dto.EmployeeDocument);
            sale.Payment = await _context.Payment.FindAsync(dto.PaymentId);

            if (_context.Sale == null)
            {
                return Problem("Entity set 'AndreVeiculosAPIContext.Sale'  is null.");
            }
            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }
    }
}