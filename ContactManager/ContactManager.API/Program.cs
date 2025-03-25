using ContactManager.Infrastructure.Data;
using ContactManager.Application.Interfaces;
using ContactManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IContactRepository, ContactRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🟢 Adicione essa linha ANTES do UseRouting
app.UseMetricServer();        // ← Esta linha inicia o servidor /metrics

app.UseRouting();
app.UseHttpMetrics();         // ← Coleta de métricas das requisições

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapMetrics();   // ← Expõe /metrics (pode manter como fallback)
});

app.Run();

public partial class Program { }
