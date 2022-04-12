using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ActivityTypesController : ControllerBase
    {
        private ITimelogServiceBuilder _timelogService;
        private IEntityService<ActivityType> _activityTypeManager;

        public ActivityTypesController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogServiceBuilder;
            _activityTypeManager = _timelogService.CreateActivityTypeService();
        }
        // GET: api/<ActivityTypesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityType>>> Get()
        {
            return Ok(await _activityTypeManager.GetAllAsync());
        }

        // GET api/<ActivityTypesController>/5
        [HttpGet("{guid}")]
        public async Task<ActionResult<ActivityType>> Get(Guid guid)
        {
            var activityType = await _activityTypeManager.GetByIdAsync(guid);
            if (activityType == null)
            {
                return NotFound();
            }
            return Ok(activityType);
        }

        // POST api/<ActivityTypesController>
        [HttpPost]
        public async Task<ActionResult<ActivityType>> Post(SaveActivityTypeViewModel saveActivityTypeViewModel)
        {
            if (ModelState.IsValid)
            {

                var activityType = new ActivityType() { Name = saveActivityTypeViewModel.Name, Description = saveActivityTypeViewModel.Description };

                await _activityTypeManager.CreateAsync(activityType);
                return Ok(activityType);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<ActivityTypesController>/5
        [HttpPut("{guid}")]
        public async Task<ActionResult<ActivityType>> Put(string guid, SaveProjectViewModel saveProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                var activityType = new ActivityType()
                {
                    Id = new Guid(guid),
                    Name = saveProjectViewModel.Name,
                    Description = saveProjectViewModel.Description
                };
                await _activityTypeManager.UpdateAsync(activityType);
                return Ok(activityType);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ActivityTypesController>/5
        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _activityTypeManager.DeleteAsync(guid);
            return NoContent();
        }
    }
}
