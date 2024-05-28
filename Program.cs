using Microsoft.EntityFrameworkCore;
using GestiondeEventos.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("ConnectionDB");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var misReglasCORS = "ReglasCORS";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCORS, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
});

var app = builder.Build();

/* Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(misReglasCORS);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
