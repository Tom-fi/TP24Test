using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP24.Api.Invoices;

namespace TP24.Api.Summaries
{
    [Route("[controller]")]
    [ApiController]
    public class SummariesController : ControllerBase
    {
        private readonly SummaryService _summaryService;
        private readonly ILogger<InvoicesController> _logger;
        public SummariesController(ILogger<InvoicesController> logger, SummaryService summaryService)
        {
            _logger = logger;
            _summaryService = summaryService;
        }

        [HttpGet(Name = "GetSummary")]
        public async Task<IActionResult> Get()
        {
            var response = await _summaryService.GetSummaryAsync();
            if (response.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("SumByCurrency", Name = "GetSumByCurrency")]
        public async Task<IActionResult> GetSumByCurrency()
        {
            var response = await _summaryService.GetSumsAsync();
            if (response.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
