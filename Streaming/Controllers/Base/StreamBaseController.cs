using Microsoft.AspNetCore.Mvc;
using Streaming.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Streaming.Controllers.Base
{
    public class StreamBaseController : ControllerBase
    {
        protected readonly ILogger _logger;
        private readonly ConcurrentBag<StreamWriter> _streamData = new();

        public StreamBaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected async Task Send(object data)
        {
            if (data is null) return;

            foreach (var item in _streamData)
            {
                var serialized = JsonSerializer.Serialize(data);

                _logger.LogInformation($"Sending {serialized}");

                await item.WriteAsync(serialized);
                await item.FlushAsync();
            }
        }

        private void OnStreamAvailable(Stream stream, CancellationToken cancellationToken)
        {
            var waitHandle = cancellationToken.WaitHandle;
            var item = new StreamWriter(stream);

            _streamData.Add(item);

            waitHandle.WaitOne();

            _streamData.TryTake(out _);
        }

        protected IActionResult Streaming()
        {
            return new StreamResult(OnStreamAvailable);
        }
    }
}