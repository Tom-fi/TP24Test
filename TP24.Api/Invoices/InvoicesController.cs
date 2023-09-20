using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TP24.Api.Invoices.DTO;

namespace TP24.Api.Invoices
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(ILogger<InvoicesController> logger, InvoiceService service)
        {
            _logger = logger;
            _invoiceService = service;
        }

        [HttpGet(Name = "GetInvoice")]
        public async Task<IActionResult> Get([FromQuery] string reference)
        {
            if(string.IsNullOrEmpty(reference))
            {
                return BadRequest();
            }

            var response = await _invoiceService.GetAsync(reference);
            if(string.IsNullOrEmpty(response.Reference))
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllInvoices")]
        public async Task<IActionResult> GetAll()
        {
            List<GetInvoiceResponse> response = await _invoiceService.GetAllAsync();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost(Name = "AddInvoice")]

        public async Task<IActionResult> Add([FromBody] AddInvoiceRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _invoiceService.AddNewAsync(request);
                return Ok(response);

            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(Name = "UpdateInvoice")]
        public async Task<IActionResult> Update([FromBody] UpdateInvoiceRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _invoiceService.UpdateAsync(request);
            return Ok(response);
        }

        [HttpDelete(Name = "DeleteInvoice")]
        public async Task<IActionResult> Delete([FromQuery] string reference)
        {
            if(string.IsNullOrEmpty(reference))
            {
                return BadRequest();
            }
            var response = await _invoiceService.DeleteAsync(reference);
            return Ok(response);
        }
    }
}
