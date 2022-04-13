using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.CommandRequests;
using Timelog.AspNetCore.Services;
using Timelog.AspNetCore.ViewModels;
using Timelog.Core;
using Timelog.Core.Entities;
using Timelog.Core.Services;

namespace Timelog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private ITimelogServiceBuilder _timelogService;
        private IEntityService<Project> _projectManager;

        public ProjectsController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogServiceBuilder;
            _projectManager = _timelogService.CreateProjectService();
        }
        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            return Ok(await _projectManager.GetAllAsync());
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{guid}")]
        public async Task<ActionResult<Project>> Get(Guid guid)
        {
            var project = await _projectManager.GetByIdAsync(guid);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<ActionResult<ProjectViewModel>> Post(SaveProjectRequest saveProjectRequest)
        {
            if (ModelState.IsValid)
            {
                
                var project = new Project() { Name = saveProjectRequest.Name, Description = saveProjectRequest.Description };
               
                await _projectManager.CreateAsync(project);
                return Ok(project);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{guid}")]
        public async Task<ActionResult<Project>> Put(string guid, SaveProjectRequest saveProjectRequest)
        {
            if (ModelState.IsValid)
            {
                var project = new Project()
                {

                    Id = new Guid(guid),
                    Name = saveProjectRequest.Name,
                    Description = saveProjectRequest.Description
                };
                await _projectManager.UpdateAsync(project);
                return Ok(project);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _projectManager.DeleteAsync(guid);
            return NoContent();
        }
    }
}
