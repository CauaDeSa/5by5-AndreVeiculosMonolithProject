using AndreVeiculosAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.People;
using Models.PeopleDTO;

namespace AndreVeiculosAPI.Controllers.EntityFramework
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        private readonly AndreVeiculosAPIContext _context;

        public JobTitlesController(AndreVeiculosAPIContext context)
        {
            _context = context;
        }

        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<JobTitle>>> GetJobTitle()
        {
            if (_context.JobTitle == null)
            {
                return NotFound();
            }
            return await _context.JobTitle.ToListAsync();
        }

        [HttpGet("entity/{id}")]
        public async Task<ActionResult<JobTitle>> GetJobTitle(int id)
        {
            if (_context.JobTitle == null)
            {
                return NotFound();
            }
            var jobTitle = await _context.JobTitle.FindAsync(id);

            if (jobTitle == null)
            {
                return NotFound();
            }

            return jobTitle;
        }

        [HttpPost("entity")]
        public async Task<ActionResult<JobTitle>> PostJobTitle(JobTitleDTO dto)
        {
            JobTitle jobTitle = new() { Description = dto.Description};

            if (_context.JobTitle == null)
            {
                return Problem("Entity set 'AndreVeiculosAPIContext.JobTitle'  is null.");
            }
            _context.JobTitle.Add(jobTitle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTitle", new { id = jobTitle.Id }, jobTitle);
        }
    }
}
