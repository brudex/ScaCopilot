using Microsoft.AspNetCore.Mvc;
using ScaCopilot.Models;

namespace ScaCopilot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CopilotController : ControllerBase
    {
        private readonly ILogger<CopilotController> _logger;
        public CopilotController(ILogger<CopilotController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Copilot")]
        public async Task<CopilotResponse> Post([FromBody]CopilotRequest payload)
        {
            _logger.LogInformation("Copilot Request Received ");
            var response = await PromptService.Instance.Execute(payload);
            return response;
        }
    }
}
