using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.People;
using Models.PeopleDTO;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public EmployeesController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            return await _context.Employee.Include(x => x.Address).Include(x => x.Function).ToListAsync();
        }

        [HttpGet("entity/{document}")]
        public async Task<ActionResult<Employee>> GetEmployee(string document)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(document);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost("entity")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return CreatedAtAction("GetEmployee", new { id = employee.Document }, employee);
        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employee?.Any(e => e.Document == id)).GetValueOrDefault();
        }
    }
}
