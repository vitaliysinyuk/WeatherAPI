


using Microsoft.AspNetCore.Builder;
using WeatherAPI.BusinessLogic;
using WeatherAPI.BusinessLogic.Interfaces;
using WeatherAPI.Repository;
using WeatherAPI.Repository.Interfaces;
using Scrutor;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

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
