using AlertManagerFMR.Apllication;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ;
using DbAdapter;
using InfoProperty;
using Microsoft.EntityFrameworkCore;

namespace AlertManagerFMR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertManagerController : ControllerBase
    {

        private readonly ILogger<AlertManagerController> _logger;
        private ApplicationService _applicationService;
        private RabbitMqFactory _rabbitMQ;
        private ClientRuleContext _clientRuleContext;

        public AlertManagerController(ILogger<AlertManagerController> logger, ApplicationService applicationService, RabbitMqFactory rabbitMqFactory,ClientRuleContext clientRuleContext)
        {
            _logger = logger;
            _applicationService = applicationService;
            _rabbitMQ = rabbitMqFactory;
            _clientRuleContext = clientRuleContext;
            _rabbitMQ.Start();


        }

       [HttpPost(Name ="PosrRule")]
        public async Task<IActionResult> PostRule(RuleInfo ruleInfo)
        {
            _applicationService.PostRule();
            _clientRuleContext.Rules.Add(ruleInfo);
            await _clientRuleContext.SaveChangesAsync();
            await _rabbitMQ.SendMessageAsync(ruleInfo);
            return Ok();
        } 

    }
}
