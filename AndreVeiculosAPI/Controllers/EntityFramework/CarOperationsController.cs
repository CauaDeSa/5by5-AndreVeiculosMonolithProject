using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Cars;
using Models.CarsDTO;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarOperationsController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public CarOperationsController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("carOperation/entity")]
        public async Task<ActionResult<IEnumerable<CarOperation>>> GetCarOperation()
        {
            if (_context.CarOperation == null)
            {
                return NotFound();
            }
            return await _context.CarOperation.Include(c => c.Car).Include(o => o.Operation).ToListAsync();
        }

        [HttpGet("carOperation/entity/{id}")]
        public async Task<ActionResult<CarOperation>> GetCarOperation(int id)
        {
            if (_context.CarOperation == null)
            {
                return NotFound();
            }

            var carOperation = await _context.CarOperation.FindAsync(id);

            if (carOperation == null)
            {
                return NotFound();
            }

            return carOperation;
        }

        //[HttpPut("carOperation/entity/{id}")]
        //public async Task<IActionResult> PutCarOperation(int id, CarOperation carOperation)
        //{
        //    if (id != carOperation.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(carOperation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CarOperationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost("carOperation/entity")]
        public async Task<ActionResult<CarOperation>> PostCarOperation(CarOperationDTO dto)
        {
            CarOperation carOperation = new CarOperation(dto);

            carOperation.Car = await _context.Car.FindAsync(carOperation.Car.Plate);
            carOperation.Operation = await _context.Operation.FindAsync(carOperation.Operation.Id);

            _context.CarOperation.Add(carOperation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarOperation", new { id = carOperation.Id }, carOperation);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCarOperation(int id)
        //{
        //    if (_context.CarOperation == null)
        //    {
        //        return NotFound();
        //    }
        //    var carOperation = await _context.CarOperation.FindAsync(id);
        //    if (carOperation == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CarOperation.Remove(carOperation);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CarOperationExists(int id)
        //{
        //    return (_context.CarOperation?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
