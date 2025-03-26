using GymFitness.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceTokenController : Controller
    {
        private readonly IDeviceTokenService _deviceTokenService;

        public DeviceTokenController(IDeviceTokenService deviceTokenService)
        {
            _deviceTokenService = deviceTokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceTokens()
        {
            var deviceTokens = await _deviceTokenService.GetDeviceTokensAsync();
            return Ok(deviceTokens);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceToken([FromBody] string deviceToken)
        {
            var result = await _deviceTokenService.AddDeviceTokenAsync(deviceToken);
            return Ok(result);
        }
    }
}
