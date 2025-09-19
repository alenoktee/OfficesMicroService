using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces;

public interface IOfficeService
{
    Task<IEnumerable<Office>> GetAllOfficesAsync();
    Task<Office?> GetOfficeByIdAsync(string id);
    Task<Office> CreateOfficeAsync(OfficeCreateDto officeCreateDto);
    Task<bool> UpdateOfficeAsync(string id, OfficeUpdateDto officeUpdateDto);
    Task<bool> DeleteOfficeAsync(string id);
}
