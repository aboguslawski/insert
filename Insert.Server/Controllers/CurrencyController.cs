using Insert.Server.Models.Entities;
using Insert.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insert.Server.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CurrencyController(CurrencyService currencyService)
    {
        private readonly CurrencyService _currencyService = currencyService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> Get() 
        {
            try
            {
                var currencies = await _currencyService.GetCurrencies();
                return currencies;
            }
            catch (Exception ex)
            {
                // logs
                return new NotFoundResult();
            }
        }
    }
}
