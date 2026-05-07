namespace KazanRealEstate.Api.Repositories.Tables;

public class Favorite
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public int ResidentialComplexId { get; set; }
    public ResidentialComplex? ResidentialComplex { get; set; }
    
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
