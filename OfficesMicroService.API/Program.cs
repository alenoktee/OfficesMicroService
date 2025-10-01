using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using OfficesMicroService.API.Endpoints;
using OfficesMicroService.API.Middleware;
using OfficesMicroService.Application;
using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Application.Exceptions;
using OfficesMicroService.Application.Interfaces.Repositories;
using OfficesMicroService.Application.Interfaces.Services;
using OfficesMicroService.Application.Mapping;
using OfficesMicroService.Application.Services;
using OfficesMicroService.Application.Validators;
using OfficesMicroService.Infrastructure;
using OfficesMicroService.Infrastructure.Repositories;

using Polly;
using Polly.CircuitBreaker;

namespace OfficesMicroService;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(sp.GetRequiredService<IOptions<MongoDbSettings>>().Value.ConnectionString));

        builder.Services.AddSingleton<IOfficeRepository, OfficeRepository>();
        builder.Services.AddScoped<IOfficeService, OfficeService>();

        builder.Services.AddValidatorsFromAssemblyContaining<OfficeCreateDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<OfficeUpdateDtoValidator>();

        builder.Services.AddSingleton<AsyncCircuitBreakerPolicy>(
            Policy
                .Handle<Exception>(ex => ex is MongoException or DuplicateAddressException or NotFoundException)
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(60),
                    onBreak: (ex, breakDelay) => {
                        Console.WriteLine($"Circuit breaker tripped due to: {ex.Message}. Breaking for {breakDelay.TotalSeconds} seconds.");
                    },
                    onReset: () => {
                        Console.WriteLine("Circuit breaker reset.");
                    }
                )
        );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var mongoClient = services.GetRequiredService<IMongoClient>();
            var mongoSettings = services.GetRequiredService<IOptions<MongoDbSettings>>();
            await MongoDbIndexInitializer.CreateUniqueOfficeAddressIndex(mongoClient, mongoSettings);
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<PollyCircuitBreakerMiddleware>();

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
