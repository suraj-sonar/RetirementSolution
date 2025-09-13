using Microsoft.AspNetCore.Mvc;

namespace Base.Api.Middleware
{
    public class ErrorDetails: ProblemDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public IDictionary<String, string[]> Errors { get; set; } = new Dictionary<String, string[]>();

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
