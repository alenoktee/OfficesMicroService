using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces.Repositories;

public interface IOfficeRepository : IGenericRepository<Office>
{
    Task<IEnumerable<Office>> GetByCityAsync(string city, CancellationToken cancellationToken = default);
    Task<bool> DoesAddressExistAsync(string city, string street, string houseNumber, CancellationToken cancellationToken = default);
    Task<bool> UpdateDetailsAsync(string id, OfficeUpdateDto dto, CancellationToken cancellationToken = default);
}
