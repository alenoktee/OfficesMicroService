using AutoMapper;

using MongoDB.Driver;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Application.Exceptions;
using OfficesMicroService.Application.Interfaces.Repositories;
using OfficesMicroService.Application.Interfaces.Services;
using OfficesMicroService.Domain.Entities;

using System.Collections.Concurrent;
using System.Threading;

namespace OfficesMicroService.Application.Services;

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public OfficeService(IOfficeRepository officeRepository, IMapper mapper)
    {
        _officeRepository = officeRepository;
        _mapper = mapper;
    }

    public async Task<OfficeDto> CreateAsync(OfficeCreateDto officeCreateDto, CancellationToken cancellationToken = default)
    {
        var office = _mapper.Map<Office>(officeCreateDto);
        try
        {
            await _officeRepository.CreateAsync(office, cancellationToken);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            throw new DuplicateAddressException("An office with this address already exists.");
        }

        return _mapper.Map<OfficeDto>(office);
    }

    public async Task<IEnumerable<OfficeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var offices = await _officeRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<OfficeDto>>(offices);
    }

    public async Task<OfficeDto> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var office = await _officeRepository.GetByIdAsync(id, cancellationToken);
        if (office is null)
        {
            throw new NotFoundException($"Office with id '{id}' not found.");
        }
        return _mapper.Map<OfficeDto>(office);
    }

    public async Task<IEnumerable<OfficeDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var offices = await _officeRepository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        return _mapper.Map<IEnumerable<OfficeDto>>(offices);
    }

    public async Task UpdateAsync(string id, OfficeUpdateDto officeUpdateDto, CancellationToken cancellationToken = default)
    {
        var office = await GetByIdAsync(id);
        _mapper.Map(officeUpdateDto, office);
        await _officeRepository.UpdateAsync(office, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _officeRepository.DeleteAsync(id, cancellationToken);
        if (!result)
        {
            throw new NotFoundException($"Office with id '{id}' not found.");
        }
    }
}
