using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TeamController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET /api/Team
        [HttpGet]
        [SwaggerOperation(Summary = "Get all teams", Description = "Returns a list of all teams.")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            if (_dataContext.Teams == null)
            {
                return NotFound();
            }

            return await _dataContext.Teams.ToListAsync();
        }

        // GET /api/Team/{id}
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Get a team by ID", Description = "Returns a single team based on the provided ID.")]
        public async Task<ActionResult<Team>> GetTeam(int Id)
        {
            if (_dataContext.Teams == null)
            {
                return NotFound();
            }
            var team = await _dataContext.Teams.FindAsync(Id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        // POST /api/Team
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new team", Description = "Creates a new team and returns the created team.")]
        public async Task<ActionResult<Team>> AddTeam(Team team)
        {
            _dataContext.Teams.Add(team);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        // PUT /api/Team/{id}
        [HttpPut("{Id:int}")]
        [SwaggerOperation(Summary = "Update an existing team", Description = "Updates an existing team based on the provided ID.")]
        public async Task<IActionResult> UpdateTeam(int Id, Team team)
        {
            if (Id != team.Id)
            {
                return BadRequest();
            }
            _dataContext.Entry(team).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(Id))
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

        // DELETE /api/Team/{id}
        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete a team", Description = "Deletes an existing team based on the provided ID.")]
        public async Task<IActionResult> DeleteTeam(int Id)
        {
            if (_dataContext.Teams == null)
            {
                return NotFound();
            }
            var team = await _dataContext.Teams.FindAsync(Id);
            if (team == null)
            {
                return NotFound();
            }
            _dataContext.Teams.Remove(team);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _dataContext.Teams.Any(e => e.Id == id);
        }
    }
}