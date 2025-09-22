using Microsoft.AspNetCore.Http.HttpResults;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Application.Interfaces.Services;

namespace OfficesMicroService.API.Endpoints;

public static class OfficeEndpoints
{
    public static void MapOfficeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/offices").WithTags("Offices");

        group.MapGet("/", async Task<IResult> (IOfficeService service, CancellationToken ct) =>
        {
            var offices = await service.GetAllAsync(ct);
            return Results.Ok(offices);
        });

        group.MapGet("/{id}", async (string id, IOfficeService service, CancellationToken ct) =>
        {
            var office = await service.GetByIdAsync(id, ct);
            return Results.Ok(office);
        });

        group.MapPost("/", async (OfficeCreateDto officeCreateDto, IOfficeService service, CancellationToken ct) =>
        {
            var newOffice = await service.CreateAsync(officeCreateDto, ct);
            return Results.Created($"/offices/{newOffice.Id}", newOffice);
        })
        .AddEndpointFilter<ValidationFilter<OfficeCreateDto>>();

        group.MapPut("/{id}", async (string id, OfficeUpdateDto officeUpdateDto, IOfficeService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, officeUpdateDto, ct);
            return Results.NoContent();
        })
        .AddEndpointFilter<ValidationFilter<OfficeUpdateDto>>();

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
