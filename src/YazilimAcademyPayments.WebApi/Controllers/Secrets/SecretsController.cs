using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YazilimAcademyPayments.WebApi.Settings;

namespace YazilimAcademyPayments.WebApi.Controllers.Secrets
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        private readonly PayTRSettings _paytrSettings;

        public SecretsController(PayTRSettings paytrSettings)
        {
            _paytrSettings = paytrSettings;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_paytrSettings);
        }
    }
}
