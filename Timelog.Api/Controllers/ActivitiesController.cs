using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timelog.Api.Responses;
using Timelog.AspNetCore.CommandRequests;
using Timelog.AspNetCore.Services;
using Timelog.AspNetCore.ViewModels;
using Timelog.Core;
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

        // GET api/<ActivitiesController>/Current
        [HttpGet("Current")]
        public async Task<ActionResult<UserActivityViewModel>> GetCurrent()
        {
            var activity = await _activityManager.GetCurrentActivityIfExistAsync();
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        // POST api/<ActivitiesController>/Start
        [HttpPost("Start")]
        public async Task<ActionResult<ApiResponse>> Start(StartActivityRequest startRequest)
        {
            if (ModelState.IsValid)
            {                            

                await _activityManager.StartNewActivityAsync(startRequest.ProjectId, startRequest.ActivityTypeId);
                var currentActivity = await _activityManager.GetCurrentActivityIfExistAsync();

                return Ok(new ApiResponse(ResponseStatus.success, "Activity started", currentActivity));
            }
            else
            {
                return BadRequest(new ApiResponse(ResponseStatus.error, ModelState.ErrorCount.ToString()));
            }
        }
        // POST api/<ActivitiesController>/Start
        [HttpPost("Stop")]
        public async Task<ActionResult<ApiResponse>> Stop(StopCurrentActivityRequest stopRequest)
        {
            var currentActivity = await _activityManager.GetCurrentActivityIfExistAsync();
            if (currentActivity != null)
            {


                await _activityManager.StopCurrentActivityIfExistAsync(stopRequest.Comment);

                return Ok(new ApiResponse(ResponseStatus.success, $"Stopped with comment: '{stopRequest.Comment}'"));
            }
            else
            {
                return BadRequest(new ApiResponse(ResponseStatus.error, "no started activity"));
            }
        }
    }
}
