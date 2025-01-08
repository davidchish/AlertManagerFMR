using AlertManagerFMR.Apllication;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ;

namespace AlertManagerFMR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertManagerController : ControllerBase
    {

        private readonly ILogger<AlertManagerController> _logger;
        private ApplicationService _applicationService;
        private RabbitMqFactory _rabbitMQ;

        public AlertManagerController(ILogger<AlertManagerController> logger, ApplicationService applicationService, RabbitMqFactory rabbitMqFactory, ClientRuleContext clientRuleContext)
        {
            _logger = logger;
            _applicationService = applicationService;
            _rabbitMQ = rabbitMqFactory;


        }

       [HttpPost(Name ="")]
        public Task PostRule()
        {

        } 

    }
}
