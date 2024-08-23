using AspWebApiSecuriter;
using AspWebApiSecuriter.Endpoint;
using AspWebApiSecuriter.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using AspWebApiSecuriter.Data.Models;

var builder = WebApplication.CreateBuilder();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services
    .AddDbContext<ApiDbContext>(opt =>
    opt.UseSqlite(builder.Configuration
    .GetConnectionString("sqlite")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IPersonService, EfCorePersonneservice>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


 
await app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<ApiDbContext>().Database
    .MigrateAsync();
 
  
app.MapPersonEndpoint();

 


app.Run();
