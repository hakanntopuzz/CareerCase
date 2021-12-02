using CareerCase.Core.Services.Interfaces;
using CareerCase.Domain.Requests.Job;
using CareerCase.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerCase.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        #region Ctor
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(typeof(ServiceResult), 400)]
        public async Task<ActionResult<ServiceResult>> CreateJob(CreateJobRequest request)
        {
            var response = await _jobService.CreateJobAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("Search/{endDate}")]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        public async Task<ActionResult<ServiceResult>> SearchJob(string endDate)
        {
            var jobList = await _jobService.SearchJob(endDate);

            return Ok(jobList);
        }
    }
}