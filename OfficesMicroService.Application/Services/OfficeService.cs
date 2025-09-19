using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Application.Interfaces;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Services;

public class OfficeService : IOfficeService
{
    public Task<Office> CreateOfficeAsync(OfficeCreateDto officeCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOfficeAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Office>> GetAllOfficesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Office?> GetOfficeByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOfficeAsync(string id, OfficeUpdateDto officeUpdateDto)
    {
        throw new NotImplementedException();
    }
}
