using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTypeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProjectTypeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET /api/ProjectType
        [HttpGet]
        [SwaggerOperation(Summary = "Get all project types", Description = "Returns a list of all project types.")]
        public async Task<ActionResult<IEnumerable<ProjectType>>> GetProjectTypes()
        {
            if (_dataContext.ProjectTypes == null)
            {
                return NotFound();
            }

            return await _dataContext.ProjectTypes.ToListAsync();
        }

        // GET /api/ProjectType/{id}
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Get a project type by ID", Description = "Returns a single project type based on the provided ID.")]
        public async Task<ActionResult<ProjectType>> GetProjectType(int Id)
        {
            if (_dataContext.ProjectTypes == null)
            {
                return NotFound();
            }
            var projectType = await _dataContext.ProjectTypes.FindAsync(Id);
            if (projectType == null)
            {
                return NotFound();
            }
            return projectType;
        }

        // POST /api/ProjectType
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new project type", Description = "Creates a new project type and returns the created project type.")]
        public async Task<ActionResult<ProjectType>> AddProjectType(ProjectType projectType)
        {
            _dataContext.ProjectTypes.Add(projectType);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectType), new { id = projectType.Id }, projectType);
        }

        // PUT /api/ProjectType/{id}
        [HttpPut("{Id:int}")]
        [SwaggerOperation(Summary = "Update an existing project type", Description = "Updates an existing project type based on the provided ID.")]
        public async Task<IActionResult> UpdateProjectType(int Id, ProjectType projectType)
        {
            if (Id != projectType.Id)
            {
                return BadRequest();
            }
            _dataContext.Entry(projectType).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTypeExists(Id))
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

        // DELETE /api/ProjectType/{id}
        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete a project type", Description = "Deletes an existing project type based on the provided ID.")]
        public async Task<IActionResult> DeleteProjectType(int Id)
        {
            if (_dataContext.ProjectTypes == null)
            {
                return NotFound();
            }
            var projectType = await _dataContext.ProjectTypes.FindAsync(Id);
            if (projectType == null)
            {
                return NotFound();
            }
            _dataContext.ProjectTypes.Remove(projectType);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool ProjectTypeExists(int id)
        {
            return _dataContext.ProjectTypes.Any(e => e.Id == id);
        }
    }
}