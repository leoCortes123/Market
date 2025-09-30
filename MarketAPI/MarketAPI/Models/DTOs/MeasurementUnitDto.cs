namespace MarketAPI.Models.DTOs;

public class MeasurementUnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public bool IsWeight { get; set; }
        public int? WeightInGrams { get; set; }
    }

    public class CreateMeasurementUnitDto
    {
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public bool IsWeight { get; set; }
        public int? WeightInGrams { get; set; }
    }

    public class UpdateMeasurementUnitDto
    {
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public bool IsWeight { get; set; }
        public int? WeightInGrams { get; set; }
    }