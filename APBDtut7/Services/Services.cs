using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApbdTutorial7.Context;
using ApbdTutorial7.DTOs;
using ApbdTutorial7.Models;
using Microsoft.EntityFrameworkCore;

namespace ApbdTutorial7.Services;

public interface IPcService
{
    Task<IEnumerable<PcDto>> GetAllPcsAsync();
    Task<PcWithComponentsDto?> GetPcWithComponentsAsync(int id);
    Task<PcDto> CreatePcAsync(CreatePcDto dto);
    Task<bool> UpdatePcAsync(int id, CreatePcDto dto);
    Task<bool> DeletePcAsync(int id);
}

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PcDto>> GetAllPcsAsync()
    {
        return await _context.PCs
            .Select(p => new PcDto
            {
                Id = p.Id, Name = p.Name, Weight = p.Weight, 
                Warranty = p.Warranty, CreatedAt = p.CreatedAt, Stock = p.Stock
            }).ToListAsync();
    }

    public async Task<PcWithComponentsDto?> GetPcWithComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.Manufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.Type)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null) return null;

        return new PcWithComponentsDto
        {
            Id = pc.Id, Name = pc.Name, Weight = pc.Weight, Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt, Stock = pc.Stock,
            Components = pc.PCComponents.Select(c => new PcComponentDetailDto
            {
                Amount = c.Amount,
                Component = new ComponentDetailDto
                {
                    Code = c.Component.Code, Name = c.Component.Name, Description = c.Component.Description,
                    Manufacturer = new ManufacturerDto { Id = c.Component.Manufacturer.Id, Abbreviation = c.Component.Manufacturer.Abbreviation, FullName = c.Component.Manufacturer.FullName, FoundationDate = c.Component.Manufacturer.FoundationDate.ToString("yyyy-MM-dd") },
                    Type = new TypeDto { Id = c.Component.Type.Id, Abbreviation = c.Component.Type.Abbreviation, Name = c.Component.Type.Name }
                }
            }).ToList()
        };
    }

    public async Task<PcDto> CreatePcAsync(CreatePcDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name, Weight = dto.Weight, Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt, Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcDto { Id = pc.Id, Name = pc.Name, Weight = pc.Weight, Warranty = pc.Warranty, CreatedAt = pc.CreatedAt, Stock = pc.Stock };
    }

    public async Task<bool> UpdatePcAsync(int id, CreatePcDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePcAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return true;
    }
}