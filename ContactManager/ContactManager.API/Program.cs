using ContactManager.Infrastructure.Data;
using ContactManager.Application.Interfaces;
using ContactManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar servi�os

builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Adicione este servi�o
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configurar o DbContext com a string de conex�o
builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o reposit�rio
builder.Services.AddScoped<IContactRepository, ContactRepository>();

var app = builder.Build();

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
