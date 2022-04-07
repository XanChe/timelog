using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.Services;
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
        public async Task<IEnumerable<Project>> Get()
        {
            return await _projectManager.GetAllAsync();
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
