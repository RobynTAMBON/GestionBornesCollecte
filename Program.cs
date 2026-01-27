using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Injection du DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMesureService, MesureService>();
builder.Services.AddScoped<IBenneService, BenneService>();

builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService<MqttHostedService>();


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
