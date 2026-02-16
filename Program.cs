using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Hubs;
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

// Ajout de SignalR (WebSocket)
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});



// Build de l'application
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors("SignalRPolicy");
app.MapHub<BennesHub>("/hubs/bennes");
app.MapControllers();

app.UseAuthorization();


app.Run();
