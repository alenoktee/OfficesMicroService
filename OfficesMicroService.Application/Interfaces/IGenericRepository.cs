using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<Office>> GetAllAsync();
    Task<Office?> GetByIdAsync(string id);
    Task CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}