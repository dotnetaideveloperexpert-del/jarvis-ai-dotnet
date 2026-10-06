using Jarvis.Api.Models;

namespace Jarvis.Api.Services
{
    public class ChatService:IChatService
    {
        private readonly ILogger<ChatService> _logger;
        public ChatService(ILogger<ChatService> logger)
        {
            _logger = logger;
        }
        public async Task<ChatResponse> getreplyasnc(ChatRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Received request: {request}", request);
            // Simulate processing time
            await Task.Delay(1000, cancellationToken);
            var response = new ChatResponse
            {
                reply = $"Echo: {request.message}",
                requestid = Guid.NewGuid().ToString(),
                Timestamp = DateTime.UtcNow
            };
            _logger.LogInformation("Sending response: {response}", response);
            return response;
        }
    }
}
