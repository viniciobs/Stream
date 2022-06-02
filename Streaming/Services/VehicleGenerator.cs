using Streaming.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

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
            const string pattern = "<input.+id=\"{0}\".+value=\"(.[^\"]+)\"";
            try
            {
                var vehicle = new Vehicle();

                var match = Regex.Match(htmlSection, string.Format(pattern, "marca"));
                vehicle.Manufacturer = match.Groups[1].Value;

                match = Regex.Match(htmlSection, string.Format(pattern, "modelo"));
                vehicle.Model = match.Groups[1].Value;

                match = Regex.Match(htmlSection, string.Format(pattern, "ano"));
                vehicle.Year = Convert.ToInt32(match.Groups[1].Value);

                match = Regex.Match(htmlSection, string.Format(pattern, "placa_veiculo"));
                vehicle.VIN = match.Groups[1].Value;

                match = Regex.Match(htmlSection, string.Format(pattern, "cor"));
                vehicle.BodyColour = match.Groups[1].Value;

                return vehicle;
            }
            catch (Exception exception)
            {
                //_logger.LogError("Unable to extract vehicle from html." + Environment.NewLine + exception.Message);
                return null;
            }
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