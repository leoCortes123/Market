using MarketAPI.Custom;
using MarketAPI.Data;
using MarketAPI.Models;
using MarketAPI.Models.DTOs;
using MarketAPI.Models.DTOs.Auth;
using MarketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MarketAPI.Services;


public class MeasurementUnitService : IMeasurementUnitService
    {
        private readonly MarketDbContext _context;

        public MeasurementUnitService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MeasurementUnitDto>> GetAllMeasurementUnitsAsync()
        {
            return await _context.MeasurementUnits
                .Select(mu => new MeasurementUnitDto
                {
                    Id = mu.Id,
                    Name = mu.Name,
                    Abbreviation = mu.Abbreviation,
                    IsWeight = mu.IsWeight,
                    WeightInGrams = mu.WeightInGrams
                })
                .ToListAsync();
        }

        public async Task<MeasurementUnitDto?> GetMeasurementUnitByIdAsync(int id)
        {
            var unit = await _context.MeasurementUnits.FindAsync(id);
            if (unit == null) return null;

            return new MeasurementUnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                Abbreviation = unit.Abbreviation,
                IsWeight = unit.IsWeight,
                WeightInGrams = unit.WeightInGrams
            };
        }

        public async Task<MeasurementUnitDto> CreateMeasurementUnitAsync(CreateMeasurementUnitDto createMeasurementUnitDto)
        {
            var unit = new MeasurementUnit
            {
                Name = createMeasurementUnitDto.Name,
                Abbreviation = createMeasurementUnitDto.Abbreviation,
                IsWeight = createMeasurementUnitDto.IsWeight,
                WeightInGrams = createMeasurementUnitDto.WeightInGrams
            };

            _context.MeasurementUnits.Add(unit);
            await _context.SaveChangesAsync();

            return await GetMeasurementUnitByIdAsync(unit.Id) ?? throw new Exception("Error al crear la unidad de medida");
        }

        public async Task<MeasurementUnitDto?> UpdateMeasurementUnitAsync(int id, UpdateMeasurementUnitDto updateMeasurementUnitDto)
        {
            var unit = await _context.MeasurementUnits.FindAsync(id);
            if (unit == null) return null;

            unit.Name = updateMeasurementUnitDto.Name;
            unit.Abbreviation = updateMeasurementUnitDto.Abbreviation;
            unit.IsWeight = updateMeasurementUnitDto.IsWeight;
            unit.WeightInGrams = updateMeasurementUnitDto.WeightInGrams;

            await _context.SaveChangesAsync();

            return await GetMeasurementUnitByIdAsync(id);
        }

        public async Task<bool> DeleteMeasurementUnitAsync(int id)
        {
            var unit = await _context.MeasurementUnits
                .Include(mu => mu.Products)
                .FirstOrDefaultAsync(mu => mu.Id == id);

            if (unit == null) return false;

            // Verificar si hay productos asociados
            if (unit.Products.Any())
            {
                throw new InvalidOperationException("No se puede eliminar la unidad de medida porque tiene productos asociados");
            }

            _context.MeasurementUnits.Remove(unit);
            await _context.SaveChangesAsync();
            return true;
        }
    }