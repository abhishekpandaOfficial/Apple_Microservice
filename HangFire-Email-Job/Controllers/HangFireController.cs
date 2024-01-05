using Hangfire;
using Hangfire.Common;
using HangFire_Email_Job.Services.Email.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFire_Email_Job.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        private IEmailService _emailService;
        private IBackgroundJobClient _backgroundJobClient;
        private IRecurringJobManager _recurringJobManager;


        public HangFireController(IEmailService emailService,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
                _emailService = emailService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet]
        [Route("FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _emailService.SendEmail("Fire-and-Forget Job", DateTime.Now.ToLongTimeString()));
            return Ok();
        }

        [HttpGet]
        [Route("DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _emailService.SendEmail("Delayed Job", DateTime.Now.ToLongTimeString()), TimeSpan.FromSeconds(30));
            return Ok();
        }

        [HttpGet]
        [Route("ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _emailService.SendEmail("Recurring Job", DateTime.Now.ToLongTimeString()), Cron.Minutely);
            return Ok();
        }

        [HttpGet]
        [Route("ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var jobId = _backgroundJobClient.Enqueue(() => _emailService.SendEmail("Continuation Job 1", DateTime.Now.ToLongTimeString()));
            _backgroundJobClient.ContinueJobWith(jobId, () => _emailService.SendEmail("Continuation Job 2 - Email Reminder - ", DateTime.Now.ToLongTimeString()));
            return Ok();
        }
    }
}
