using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProjectController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET /api/Project
        [HttpGet]
        [SwaggerOperation(Summary = "Get all projects", Description = "Returns a list of all projects.")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            if (_dataContext.Projects == null)
            {
                return NotFound();
            }

            return await _dataContext.Projects.ToListAsync();
        }

        // GET /api/Project/{id}
        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Get a project by ID", Description = "Returns a single project based on the provided ID.")]
        public async Task<ActionResult<Project>> GetProject(int Id)
        {
            if (_dataContext.Projects == null)
            {
                return NotFound();
            }
            var project = await _dataContext.Projects.FindAsync(Id);
            if (project == null)
            {
                return NotFound();
            }
            return project;
        }

        // POST /api/Project
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new project", Description = "Creates a new project and returns the created project.")]
        public async Task<ActionResult<Project>> AddProject(Project project)
        {
            _dataContext.Projects.Add(project);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT /api/Project/{id}
        [HttpPut("{Id:int}")]
        [SwaggerOperation(Summary = "Update an existing project", Description = "Updates an existing project based on the provided ID.")]
        public async Task<IActionResult> UpdateProject(int Id, Project project)
        {
            if (Id != project.Id)
            {
                return BadRequest();
            }
            _dataContext.Entry(project).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Id))
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

        // DELETE /api/Project/{id}
        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete a project", Description = "Deletes an existing project based on the provided ID.")]
        public async Task<IActionResult> DeleteProject(int Id)
        {
            if (_dataContext.Projects == null)
            {
                return NotFound();
            }
            var project = await _dataContext.Projects.FindAsync(Id);
            if (project == null)
            {
                return NotFound();
            }
            _dataContext.Projects.Remove(project);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _dataContext.Projects.Any(e => e.Id == id);
        }
    }
}