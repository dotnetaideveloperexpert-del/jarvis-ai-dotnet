using Jarvis.Api.Models;

namespace Jarvis.Api.Services
{
    public interface IChatService
    {
        Task<ChatResponse> getreplyasnc(ChatRequest request, CancellationToken cancellationToken = default);
    }
}
