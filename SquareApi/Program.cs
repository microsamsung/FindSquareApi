using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SquareApi.Persistence.UOW;
using SquareApi.Persitence;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SquareApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SquareApiContext") ?? throw new InvalidOperationException("Connection string 'SquareApiContext' not found.")));
// options.UseSqlServer(builder.Configuration.GetConnectionString("SquareApiContext") ?? throw new InvalidOperationException("Connection string 'SquareApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Swagger
// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Square API",
        Description = "An API to manage points and identify possible squares"
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
