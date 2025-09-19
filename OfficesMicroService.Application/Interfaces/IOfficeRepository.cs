using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces;

public interface IOfficeRepository : IGenericRepository<Office>
{
    Task<IEnumerable<Office>> GetByCityAsync(string city);
    Task<bool> DoesAddressExistAsync(string city, string street, string houseNumber);
}
