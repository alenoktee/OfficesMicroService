using OfficesMicroService.API.Endpoints;
using OfficesMicroService.Application;
using OfficesMicroService.Application.Interfaces;
using OfficesMicroService.Application.Services;
using OfficesMicroService.Infrastructure;
using OfficesMicroService.Infrastructure.Repositories;

namespace OfficesMicroService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<IOfficeRepository, OfficeRepository>();
        builder.Services.AddScoped<IOfficeService, OfficeService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapOfficeEndpoints();

        app.Run();
    }
}
