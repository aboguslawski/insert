using Insert.Server.Models.Entities;
using Insert.Server.Models.Responses;
using Newtonsoft.Json;

namespace Insert.Server.Services
{
    public class NbpApi
    {
        private readonly HttpClient _httpClient = new();

        public async Task<Currency[]> GetCurrenciesTableA()
        {
            var message = BuildExchangeTableRequest("A");
            var response = await Send(message);
            return MapToCurrencies(response);
        }

        public async Task<Currency[]> GetCurrenciesTableB()
        {
            var message = BuildExchangeTableRequest("B");
            var response = await Send(message);
            return MapToCurrencies(response);
        }

        private HttpRequestMessage BuildExchangeTableRequest(string endpoint)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://api.nbp.pl/api/exchangerates/tables/" + endpoint)
            };
        }

        private async Task<NbpTableResponse> Send(HttpRequestMessage message)
        {

            using var response = await _httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NbpTableResponse[]>(responseBody)
                .First();
        }

        private Currency[] MapToCurrencies(NbpTableResponse response)
        {
            var currencies = response.Rates.Select(dto => new Currency
            {
                Description = dto.Currency,
                Code = dto.Code,
                Mid = dto.Mid,
                EffectiveDate = response.EffectiveDate,
                Table = response.Table
            }).ToArray();

            return currencies;
        }
    }
}
