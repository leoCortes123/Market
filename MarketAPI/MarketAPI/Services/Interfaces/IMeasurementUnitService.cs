using MarketAPI.Models.DTOs;


namespace MarketAPI.Services.Interfaces;

public interface IMeasurementUnitService
    {
        Task<IEnumerable<MeasurementUnitDto>> GetAllMeasurementUnitsAsync();
        Task<MeasurementUnitDto?> GetMeasurementUnitByIdAsync(int id);
        Task<MeasurementUnitDto> CreateMeasurementUnitAsync(CreateMeasurementUnitDto createMeasurementUnitDto);
        Task<MeasurementUnitDto?> UpdateMeasurementUnitAsync(int id, UpdateMeasurementUnitDto updateMeasurementUnitDto);
        Task<bool> DeleteMeasurementUnitAsync(int id);
    }
