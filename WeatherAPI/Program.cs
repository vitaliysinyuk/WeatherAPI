
using Microsoft.AspNetCore.Connections;
using System.Reflection;
using WeatherAPI.Models;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
Assembly assembly = Assembly.GetExecutingAssembly();

builder.Services.AddScoped<HttpClient>();


//Dependency Injection with scan using reflection
builder.Services.Scan(scan => scan.FromAssemblies(assembly)
                                .AddClasses()
        .AsMatchingInterface()
        .WithTransientLifetime());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding Automapper profiles
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(assembly);
});

//-- CORS
builder.Services.AddCors();


IConfiguration config = builder.Configuration;
//Settings
var nasaPowerSettings = new NASAPowerSettings();
nasaPowerSettings.DailyWeather = config.GetSection("NASAPower:Daily").Get<NASAPowerSettings.Daily>();

builder.Services.AddSingleton(nasaPowerSettings);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //-- CORS
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => new Uri(origin).IsLoopback));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
