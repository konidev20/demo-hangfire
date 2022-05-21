using Hangfire.Demo.Shared.Listeners;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Demo.SchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobController(ILogger<JobController> logger, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        public IActionResult ScheduleJob()
        {
            Guid jobId = Guid.NewGuid();
            _logger.LogInformation($"Enqueue Job with {jobId}");
            _backgroundJobClient.Enqueue(() => Console.WriteLine("Executed Job with JobId = " + jobId.ToString()));
            return Ok();
        }

        [HttpPost]
        [Route("alpha")]
        public IActionResult ScheduleJobToAlpha()
        {
            Guid jobId = Guid.NewGuid();
            _logger.LogInformation($"Enqueue Job to alpha with {jobId}");
            _backgroundJobClient.Enqueue<AlphaQueueListener>(a => a.Execute(jobId));
            return Ok();
        }

        [HttpPost]
        [Route("beta")]
        public IActionResult ScheduleJobToBeta()
        {
            _logger.LogInformation($"Enqueue Job to beta.");
            _backgroundJobClient.Enqueue<BetaQueueListener>(b => b.Execute());
            return Ok();
        }
    }
}
