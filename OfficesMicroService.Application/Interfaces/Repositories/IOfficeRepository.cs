using MongoDB.Driver;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces.Repositories;

public interface IOfficeRepository : IGenericRepository<Office>
{
    Task<IEnumerable<Office>> GetByCityAsync(string city, CancellationToken cancellationToken = default);
    Task<bool> PartialUpdateAsync(string id, UpdateDefinition<Office> update, CancellationToken cancellationToken = default);

}
