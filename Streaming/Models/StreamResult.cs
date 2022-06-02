using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Streaming.Models
{
    public class StreamResult : IActionResult
    {
        private readonly Action<Stream, CancellationToken> _onStreamAvailable;

        public StreamResult(Action<Stream, CancellationToken> onStreamAvailable)
        {
            _onStreamAvailable = onStreamAvailable;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var stream = context.HttpContext.Response.Body;

            context.HttpContext.Response.GetTypedHeaders().ContentType = new MediaTypeHeaderValue("text/event-stream");

            _onStreamAvailable(stream, context.HttpContext.RequestAborted);
        }
    }
}