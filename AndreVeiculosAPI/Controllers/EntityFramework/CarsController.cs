using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Cars;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public CarsController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            if (_context.Car == null)
            {
                return NotFound();
            }
            return await _context.Car.ToListAsync();
        }

        [HttpGet("entity{plate}")]
        public async Task<ActionResult<Car>> GetCar(string plate)
        {
            if (_context.Car == null)
            {
                return NotFound();
            }
            var car = await _context.Car.FindAsync(plate);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost("entity")]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'AndreVeiculosAPIContext.Car'  is null.");
            }
            _context.Car.Add(car);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.Plate))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCar", new { id = car.Plate }, car);
        }

        private bool CarExists(string plate)
        {
            return (_context.Car?.Any(e => e.Plate == plate)).GetValueOrDefault();
        }
    }
}
