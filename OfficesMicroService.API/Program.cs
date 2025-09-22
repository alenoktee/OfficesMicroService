using AutoMapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using OfficesMicroService.API.Endpoints;
using OfficesMicroService.API.Middleware;
using OfficesMicroService.Application;
using OfficesMicroService.Application.Interfaces.Repositories;
using OfficesMicroService.Application.Interfaces.Services;
using OfficesMicroService.Application.Mapping;
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

        builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(sp.GetRequiredService<IOptions<MongoDbSettings>>().Value.ConnectionString));

        builder.Services.AddSingleton<IOfficeRepository, OfficeRepository>();
        builder.Services.AddScoped<IOfficeService, OfficeService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

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
