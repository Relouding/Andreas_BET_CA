using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DeveloperController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET /api/Developer
        [HttpGet]
        [SwaggerOperation(Summary = "Get all developers", Description = "Returns a list of all developers with their roles and teams.")]
        public async Task<ActionResult<IEnumerable<object>>> GetDevelopers()
        {
            if (_dataContext.Developers == null)
            {
                return NotFound();
            }

            var developers = await _dataContext.Developers
                .Include(d => d.Role)
                .Include(d => d.Team)
                .Select(d => new 
                {
                    d.Id,
                    d.Firstname,
                    d.Lastname,
                    RoleName = d.Role != null ? d.Role.Name : null,
                    TeamName = d.Team != null ? d.Team.Name : null
                })
                .ToListAsync();

            return Ok(developers);
        }

        // GET /api/Developer/{id}
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Get a developer by ID", Description = "Returns a single developer with their role and team based on the provided ID.")]
        public async Task<ActionResult<object>> GetDeveloper(int Id)
        {
            if (_dataContext.Developers == null)
            {
                return NotFound();
            }

            var developer = await _dataContext.Developers
                .Include(d => d.Role)
                .Include(d => d.Team)
                .Where(d => d.Id == Id)
                .Select(d => new
                {
                    d.Id,
                    d.Firstname,
                    d.Lastname,
                    RoleName = d.Role != null ? d.Role.Name : null,
                    TeamName = d.Team != null ? d.Team.Name : null
                })
                .FirstOrDefaultAsync();

            if (developer == null)
            {
                return NotFound();
            }

            return Ok(developer);
        }

        // POST /api/Developer
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new developer", Description = "Creates a new developer and returns the created developer.")]
        public async Task<ActionResult<Developer>> AddDeveloper(Developer developer)
        {
            _dataContext.Developers.Add(developer);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDeveloper), new { id = developer.Id }, developer);
        }

        // PUT /api/Developer/{id}
        [HttpPut("{Id:int}")]
        [SwaggerOperation(Summary = "Update an existing developer", Description = "Updates an existing developer based on the provided ID.")]
        public async Task<IActionResult> UpdateDeveloper(int Id, Developer developer)
        {
            if (Id != developer.Id)
            {
                return BadRequest();
            }
            _dataContext.Entry(developer).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE /api/Developer/{id}
        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete a developer", Description = "Deletes an existing developer based on the provided ID.")]
        public async Task<IActionResult> DeleteDeveloper(int Id)
        {
            if (_dataContext.Developers == null)
            {
                return NotFound();
            }
            var developer = await _dataContext.Developers.FindAsync(Id);
            if (developer == null)
            {
                return NotFound();
            }
            _dataContext.Developers.Remove(developer);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool DeveloperExists(int id)
        {
            return _dataContext.Developers.Any(e => e.Id == id);
        }
    }
}