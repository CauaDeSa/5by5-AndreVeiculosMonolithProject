using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public PaymentsController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }
            return await _context.Payment.Include(x => x.Fetlock).Include(x => x.Card).Include(x => x.Pix.Type).Include(x => x.Pix).ToListAsync();
        }

        [HttpGet("entity/{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }
            var payment = await _context.Payment.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpPost("entity")]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'AndreVeiculosAPIContext.Payment'  is null.");
            }
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
        }
    }
}