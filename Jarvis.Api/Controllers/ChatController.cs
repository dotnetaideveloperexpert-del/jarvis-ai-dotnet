using Jarvis.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jarvis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly Services.IChatService _chatService;
        private readonly ILogger<ChatController> _logger;
        public ChatController( Services.IChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;   
        }
        [HttpPost]
        [ProducesResponseType(typeof(ChatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Chat(
        [FromBody] ChatRequest request,
        CancellationToken cancellationToken)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.message))
            {
                _logger.LogWarning("Empty message received");
                return BadRequest(new { error = "Message cannot be empty." });
            }

            var response = await _chatService.getreplyasnc(request, cancellationToken);
            return Ok(response);
        }
    }
}
