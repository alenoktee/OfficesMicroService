using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Application.Interfaces.Services;
using OfficesMicroService.Domain.Entities;

using System.ComponentModel.DataAnnotations;

namespace OfficesMicroService.API.Endpoints;

public static class OfficeEndpoints
{
    public static void MapOfficeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/offices").WithTags("Offices");

        group.MapGet("/", async Task<IResult> (IOfficeService service, CancellationToken ct) =>
        {
            var offices = await service.GetAllAsync(ct);
            return offices is not null ? Results.Ok(offices) : Results.NotFound();
        });

        group.MapGet("/{id}", async (string id, IOfficeService service, CancellationToken ct) =>
        {
            var office = await service.GetByIdAsync(id, ct);
            return office is not null ? Results.Ok(office) : Results.NotFound();
        });

        group.MapPost("/", async (OfficeCreateDto officeCreateDto, IOfficeService service, CancellationToken ct) =>
        {
            var office = await service.CreateAsync(officeCreateDto, ct);
            return Results.Created($"/offices/{office.Id}", office);
        })
        .AddEndpointFilter(new ValidationFilter<OfficeCreateDto>());

        group.MapPut("/{id}", async (string id, OfficeUpdateDto officeUpdateDto, IOfficeService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, officeUpdateDto, ct);
            return Results.NoContent();
        })
        .AddEndpointFilter(new ValidationFilter<OfficeUpdateDto>());

        group.MapDelete("/{id}", async (string id, IOfficeService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        group.MapGet("/paged", async (int pageNumber, int pageSize, IOfficeService service, CancellationToken ct) =>
        {
            var offices = await service.GetPagedAsync(pageNumber, pageSize, ct);
            return Results.Ok(offices);
        });
    }
}
