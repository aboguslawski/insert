using Insert.Server.Context;
using Insert.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insert.Server.Services
{
    public class CurrencyService(IServiceScopeFactory serviceProvider)
    {
        private readonly IServiceScopeFactory _serviceProvider = serviceProvider;

        public async Task<List<Currency>> GetCurrencies()
        {
            using var scope = _serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetService<InsertContext>();

            return await context.Currencies.ToListAsync();
        }
    }
}
