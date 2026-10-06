namespace Jarvis.Api.Models
{
    public record ChatRequest
    {
       public string message;
        public string? userId=null;
    }
}
