using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces.Services;

public interface IOfficeService
{
    Task<IEnumerable<OfficeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<OfficeDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<OfficeDto> GetByIdAsync(string id, CancellationToken cancellationToken = default); // <-- Убираем '?'
    Task<OfficeDto> CreateAsync(OfficeCreateDto officeCreateDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(string id, OfficeUpdateDto officeUpdateDto, CancellationToken cancellationToken = default); // <-- Меняем bool на void
    Task DeleteAsync(string id, CancellationToken cancellationToken = default); // <-- Меняем bool на void
}
