using Insert.Server.Context;
using Insert.Server.Models.Entities;
using Insert.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Insert.Server.Workers
{
    public class NbpCurrencyWorker(
        NbpApi nbpApi,
        IServiceScopeFactory serviceProvider
        ) : BackgroundService
    {
        private readonly NbpApi _nbpApi = nbpApi;
        private readonly IServiceScopeFactory _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Work();
                }
                catch
                {
                    // logs
                }
                await Task.Delay(TimeSpan.FromHours(1));
            }
        }

        private async Task Work()
        {
            var tableA = _nbpApi.GetCurrenciesTableA();
            var tableB = _nbpApi.GetCurrenciesTableB();
            await Task.WhenAll(tableA, tableB);

            var nbpCurrencyData = new List<Currency>(await tableA);
            nbpCurrencyData.AddRange(await tableB);

            using var scope = _serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetService<InsertContext>();

            var existingCurrencies = await context.Currencies.ToListAsync();

            foreach (var currency in nbpCurrencyData)
            {
                var entityToUpdate = existingCurrencies.FirstOrDefault(c => c.Code == currency.Code);
                if (entityToUpdate != null)
                {
                    entityToUpdate.Description = currency.Description;
                    entityToUpdate.Mid = currency.Mid;
                    entityToUpdate.EffectiveDate = currency.EffectiveDate;
                    entityToUpdate.Table = currency.Table;
                    context.Update(entityToUpdate);
                }
                else
                {
                    context.Add(currency);
                }
            }
            await context.SaveChangesAsync();
        }


    }
}
