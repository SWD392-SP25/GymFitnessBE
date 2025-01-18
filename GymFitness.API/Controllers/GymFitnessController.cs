using Microsoft.AspNetCore.Mvc;
using GymFitness.Domain.Abstractions.Services;
using System.Text.Json;

namespace GymFitness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymFitnessController : ControllerBase
    {
        private readonly IChatCompletionService _chatService;
        private readonly ILogger<GymFitnessController> _logger;

        public GymFitnessController(IChatCompletionService chatService, ILogger<GymFitnessController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] QuestionRequest request)
        {
            try
            {
                var response = await _chatService.GetChatCompletionAsync(request.Question);
                _logger.LogInformation($"Response from service: {response}");
                
                return Ok(new { 
                    success = true,
                    message = response 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Ask endpoint: {ex.Message}");
                return StatusCode(500, new { 
                    success = false,
                    error = ex.Message 
                });
            }
        }
    }

    public class QuestionRequest
    {
        public string Question { get; set; } = string.Empty;
    }
} 