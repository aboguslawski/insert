using Insert.Server.Context;
using Insert.Server.Services;
using Insert.Server.Workers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InsertContext>(options => options.UseSqlServer("Data Source=localhost;Initial Catalog=Insert;Persist Security Info=True; Integrated Security=SSPI;TrustServerCertificate=True"));

builder.Services.AddSingleton<NbpApi>();
builder.Services.AddScoped<CurrencyService>();
builder.Services.AddHostedService<NbpCurrencyWorker>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
