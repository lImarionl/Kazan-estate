using System.ComponentModel.DataAnnotations;

namespace KazanRealEstate.Api.Repositories.Tables;

public class Developer
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public double Rating { get; set; }
    
    public List<ResidentialComplex> Projects { get; set; } = new();
}

public class ResidentialComplex
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string District { get; set; } = string.Empty; // Район Казани
    
    public string Address { get; set; } = string.Empty;
    
    public string Class { get; set; } = string.Empty; // Эконом, Комфорт, Бизнес
    
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    
    public DateTime? CompletionDate { get; set; }
    
    public int DeveloperId { get; set; }
    public Developer? Developer { get; set; }
    
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }

    // Дополнительные параметры
    public double DistanceToCenter { get; set; } // км
    public double InfrastructureRating { get; set; } // 1-5
    public double EcologicalRating { get; set; } // 1-5
    public string BuildingMaterial { get; set; } = "Monolith";
}
