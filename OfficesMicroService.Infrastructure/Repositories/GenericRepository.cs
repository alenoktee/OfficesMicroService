using Microsoft.Extensions.Options;

using MongoDB.Driver;

using OfficesMicroService.Application.Interfaces;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> _collection;

    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Office>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Office?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
