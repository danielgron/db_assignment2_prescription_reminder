using Microsoft.AspNetCore.Mvc;

namespace RenewalService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenewalController : ControllerBase
    {

        private readonly ILogger<RenewalController> _logger;
        private readonly IRenewalService _renewalService;

        public RenewalController(ILogger<RenewalController> logger, IRenewalService renewalService)
        {
            _logger = logger;
            _renewalService = renewalService;
        }

        [HttpGet(Name = "TriggerRenewals")]
        public Task Get()
        {
            _renewalService.NotifyRenewals();
            return Task.CompletedTask;
        }
    }
}