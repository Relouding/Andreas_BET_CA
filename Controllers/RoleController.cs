using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RoleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET /api/Role
        [HttpGet]
        [SwaggerOperation(Summary = "Get all roles", Description = "Returns a list of all roles.")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            if (_dataContext.Roles == null)
            {
                return NotFound();
            }

            return await _dataContext.Roles.ToListAsync();
        }

        // GET /api/Role/{id}
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Get a role by ID", Description = "Returns a single role based on the provided ID.")]
        public async Task<ActionResult<Role>> GetRole(int Id)
        {
            if (_dataContext.Roles == null)
            {
                return NotFound();
            }
            var role = await _dataContext.Roles.FindAsync(Id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        // POST /api/Role
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new role", Description = "Creates a new role and returns the created role.")]
        public async Task<ActionResult<Role>> AddRole(Role role)
        {
            _dataContext.Roles.Add(role);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        // PUT /api/Role/{id}
        [HttpPut("{Id:int}")]
        [SwaggerOperation(Summary = "Update an existing role", Description = "Updates an existing role based on the provided ID.")]
        public async Task<IActionResult> UpdateRole(int Id, Role role)
        {
            if (Id != role.Id)
            {
                return BadRequest();
            }
            _dataContext.Entry(role).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Id))
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

        // DELETE /api/Role/{id}
        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete a role", Description = "Deletes an existing role based on the provided ID.")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            if (_dataContext.Roles == null)
            {
                return NotFound();
            }
            var role = await _dataContext.Roles.FindAsync(Id);
            if (role == null)
            {
                return NotFound();
            }
            _dataContext.Roles.Remove(role);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _dataContext.Roles.Any(e => e.Id == id);
        }
    }
}