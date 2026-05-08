using Microsoft.EntityFrameworkCore;
using KazanRealEstate.Api.Repositories.Tables;

namespace KazanRealEstate.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<ResidentialComplex> ResidentialComplexes => Set<ResidentialComplex>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Favorite> Favorites => Set<Favorite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Developer>().HasData(
            new Developer { Id = 1, Name = "Унистрой", Rating = 4.8, Description = "Лидер рынка Татарстана, известный своими проектами 'Царево Village' и 'Весна'." },
            new Developer { Id = 2, Name = "Ак Барс Дом", Rating = 4.5, Description = "Один из старейших застройщиков региона с широким портфелем проектов." },
            new Developer { Id = 3, Name = "СМУ-88", Rating = 4.7, Description = "Застройщик, специализирующийся на современной архитектуре и качественной среде." },
            new Developer { Id = 4, Name = "КамаСтройИнвест", Rating = 4.9, Description = "Бутик-застройщик, работающий с историческим центром и элитной недвижимостью." }
        );

        modelBuilder.Entity<ResidentialComplex>().HasData(
            new ResidentialComplex 
            { 
                Id = 1, Name = "ЖК Царево Village", District = "Пестречинский", Class = "Эко-поселок", DeveloperId = 1, 
                MinPrice = 4500000, MaxPrice = 8500000,
                Description = "Уютный пригородный поселок с парками, школами и уникальной атмосферой.",
                ImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be?q=80&w=800",
                CompletionDate = new DateTime(2025, 12, 1),
                DistanceToCenter = 15.5, InfrastructureRating = 3.5, EcologicalRating = 4.8, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 2, Name = "ЖК Весна", District = "Советский", Class = "Комфорт+", DeveloperId = 1, 
                MinPrice = 6500000, MaxPrice = 13000000,
                Description = "Большой семейный комплекс с развитой инфраструктурой и закрытыми дворами.",
                ImageUrl = "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?q=80&w=800",
                CompletionDate = new DateTime(2026, 6, 1),
                DistanceToCenter = 9.2, InfrastructureRating = 4.2, EcologicalRating = 4.0, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 3, Name = "ЖК Мой Ритм", District = "Советский", Class = "Бизнес-лайт", DeveloperId = 2, 
                MinPrice = 7500000, MaxPrice = 16000000,
                Description = "Современный жилой массив рядом с ТЦ Мега и будущей станцией метро.",
                ImageUrl = "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?q=80&w=800",
                CompletionDate = new DateTime(2026, 3, 1),
                DistanceToCenter = 7.5, InfrastructureRating = 4.5, EcologicalRating = 3.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 4, Name = "ЖК Savin House", District = "Ново-Савиновский", Class = "Бизнес", DeveloperId = 3, 
                MinPrice = 15000000, MaxPrice = 45000000,
                Description = "Премиальный комплекс с видом на Казанку и Кремль в самом центре Кварталов.",
                ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?q=80&w=800",
                CompletionDate = new DateTime(2025, 9, 1),
                DistanceToCenter = 3.2, InfrastructureRating = 4.8, EcologicalRating = 3.8, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 5, Name = "ЖК Atlantis Deluxe", District = "Кировский", Class = "Элит", DeveloperId = 3, 
                MinPrice = 18000000, MaxPrice = 60000000,
                Description = "Стеклянные башни на берегу реки с панорамным остеклением и яхт-клубом.",
                ImageUrl = "https://images.unsplash.com/photo-1475855581690-804d4628733c?q=80&w=800",
                CompletionDate = new DateTime(2024, 12, 1),
                DistanceToCenter = 2.5, InfrastructureRating = 4.7, EcologicalRating = 3.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 6, Name = "ЖК Vincent", District = "Вахитовский", Class = "Элит", DeveloperId = 4, 
                MinPrice = 25000000, MaxPrice = 80000000,
                Description = "Клубный дом в историческом центре Казани рядом с парком 'Черное озеро'.",
                ImageUrl = "https://images.unsplash.com/photo-1460317442991-0ec23938714b?q=80&w=800",
                CompletionDate = new DateTime(2025, 6, 1),
                DistanceToCenter = 0.5, InfrastructureRating = 5.0, EcologicalRating = 4.2, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 7, Name = "ЖК Легенда", District = "Приволжский", Class = "Комфорт", DeveloperId = 2, 
                MinPrice = 6000000, MaxPrice = 12000000,
                Description = "Яркий жилой комплекс рядом со станцией метро 'Аметьево'.",
                ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?q=80&w=800",
                CompletionDate = new DateTime(2025, 3, 1),
                DistanceToCenter = 6.0, InfrastructureRating = 4.0, EcologicalRating = 3.0, BuildingMaterial = "Monolith"
            }
        );
    }
}
