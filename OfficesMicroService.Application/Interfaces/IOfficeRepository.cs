using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces;

public interface IOfficeRepository
{
    Task<IEnumerable<Office>> GetAllAsync();
    Task<Office?> GetByIdAsync(string id);

}