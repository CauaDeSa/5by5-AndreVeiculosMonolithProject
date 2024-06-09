using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Cars;
using Models.CarsDTO;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public OperationsController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperation()
        {
            if (_context.Operation == null)
            {
                return NotFound();
            }
            return await _context.Operation.ToListAsync();
        }

        [HttpGet("entity/{id}")]
        public async Task<ActionResult<Operation>> GetOperation(int id)
        {
            if (_context.Operation == null)
            {
                return NotFound();
            }
            var operation = await _context.Operation.FindAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            return operation;
        }

        [HttpPost("entity")]
        public async Task<ActionResult<Operation>> PostOperation(OperationDTO dto)
        {
            Operation operation = new(dto);

            if (_context.Operation == null)
            {
                return Problem("Entity set 'AndreVeiculosAPIContext.Operation'  is null.");
            }
            _context.Operation.Add(operation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperation", new { id = operation.Id }, operation);
        }
    }
}
