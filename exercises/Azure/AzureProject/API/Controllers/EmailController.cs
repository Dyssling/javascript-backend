using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _service;

        public EmailController(EmailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail(EmailModel model)
        {
            var result = await _service.AddNewEmailAsync(model.Email);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
