using Streaming.Models;
using System.Text.Json;

namespace Streaming.Services
{
    public class VehicleGenerator : IItemGenerator
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _httpRequestMessage;

        public VehicleGenerator()
        {
            //_logger = logger;

            _httpClient = new HttpClient();

            var content = new[]
            {
                new KeyValuePair<string, string>("acao", "gerar_veiculo"),
                new KeyValuePair<string, string>("pontuacao", "S")
            };

            _httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://www.4devs.com.br/ferramentas_online.php"),
                Content = new FormUrlEncodedContent(content)
            };
        }

        private Vehicle ExtractVehicleFromHTML(string htmlSection)
        {
            return default;
        }

        public async Task<object> GenerateOneAsync()
        {
            //_logger.LogInformation("Generating vehicle");

            var response = await _httpClient.SendAsync(_httpRequestMessage);
            var content = await response.Content.ReadAsStringAsync();
            var vehicle = ExtractVehicleFromHTML(content);

            return vehicle;
        }
    }
}