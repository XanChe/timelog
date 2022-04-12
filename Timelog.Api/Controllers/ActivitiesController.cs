using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ActivitiesController : ControllerBase
    {
        private ITimelogServiceBuilder _timelogService;
        private IUserActivityService _activityManager;

        public ActivitiesController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogServiceBuilder;
            _activityManager = _timelogService.CreateUserActivityService();
        }

        // GET: api/<ActivitiesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivityViewModel>>> Get()
        {
            return Ok(await _activityManager.GetAllAsync());
        }

        // GET api/<ActivitiesController>/5
        [HttpGet("{guid}")]
        public async Task<ActionResult<UserActivityViewModel>> Get(Guid guid)
        {
            var activity = await _activityManager.GetByIdAsync(guid);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        // POST api/<ActivitiesController>/Start
        [HttpPost("Start")]
        public async Task<ActionResult<UserActivityViewModel>> Start()
        {
            if (ModelState.IsValid)
            {
                              

                 await _activityManager.StartNewActivityAsync(new Guid("3c83bdbd-08bd-4d64-a8cb-ee947fe2a19c"), new Guid("50276063-9a51-48d0-8128-ddc8248bb746"));
                return Ok(await _activityManager.GetCurrentActivityIfExistAsync());
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
