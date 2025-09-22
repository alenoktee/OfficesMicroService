using AutoMapper;

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
        var addressExists = await _officeRepository.DoesAddressExistAsync(officeCreateDto.City, officeCreateDto.Street, officeCreateDto.HouseNumber, cancellationToken);
        if (addressExists)
        {
            throw new DuplicateAddressException("An office with this address already exists.");
        }

        var office = _mapper.Map<Office>(officeCreateDto);
        await _officeRepository.CreateAsync(office, cancellationToken);
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
        var office = await _officeRepository.GetByIdAsync(id, cancellationToken);
        if (office is null)
        {
            throw new NotFoundException($"Office with id '{id}' not found.");
        }
        await _officeRepository.UpdateDetailsAsync(id, officeUpdateDto, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var office = await _officeRepository.GetByIdAsync(id, cancellationToken);
        if (office is null)
        {
            throw new NotFoundException($"Office with id '{id}' not found.");
        }
        await _officeRepository.DeleteAsync(id, cancellationToken);
    }
}
