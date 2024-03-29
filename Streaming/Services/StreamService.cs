﻿namespace Streaming.Services
{
    public class StreamService : IStreamService
    {
        private readonly ILogger _logger;
        private readonly IItemGenerator _itemGenerator;

        public StreamService(ILogger<StreamService> logger, IItemGenerator itemGenerator)
        {
            _logger = logger;
            _itemGenerator = itemGenerator;
        }

        public async Task Stream(Action<object> sendAction, CancellationToken cancellationToken)
        {
            var random = new Random();
            var itemsQuantity = random.Next(1000, 9999);

            do
            {
                _logger.LogInformation("Generating data to stream");

                var item = await _itemGenerator.GenerateOneAsync();

                sendAction(item);

                itemsQuantity--;
            }
            while (itemsQuantity > 0 && cancellationToken.IsCancellationRequested is false);

            _logger.LogInformation("Finished stream");
        }
    }
}