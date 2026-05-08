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
                ImageUrl = "https://avatars.mds.yandex.net/get-altay/6145759/2a0000018351980ebe3b972ca5e8b8186cad/XXXL",
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
                ImageUrl = "https://avatars.mds.yandex.net/get-altay/9714262/2a0000018a89ad2ce0ff4875a462eb087aee/L",
                CompletionDate = new DateTime(2026, 3, 1),
                DistanceToCenter = 7.5, InfrastructureRating = 4.5, EcologicalRating = 3.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 4, Name = "ЖК Savin House", District = "Ново-Савиновский", Class = "Бизнес", DeveloperId = 3, 
                MinPrice = 15000000, MaxPrice = 45000000,
                Description = "Премиальный комплекс с видом на Казанку и Кремль в самом центре Кварталов.",
                ImageUrl = "https://avatars.mds.yandex.net/get-altay/1027639/2a00000187041eef8f0ee9fc06a83a921e8a/XXXL",
                CompletionDate = new DateTime(2025, 9, 1),
                DistanceToCenter = 3.2, InfrastructureRating = 4.8, EcologicalRating = 3.8, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 5, Name = "ЖК Atlantis Deluxe", District = "Кировский", Class = "Элит", DeveloperId = 3, 
                MinPrice = 18000000, MaxPrice = 60000000,
                Description = "Стеклянные башни на берегу реки с панорамным остеклением и яхт-клубом.",
                ImageUrl = "https://avatars.mds.yandex.net/get-altay/7044542/2a00000182eadb9ef479397f71366eaeb81c/XXXL",
                CompletionDate = new DateTime(2024, 12, 1),
                DistanceToCenter = 2.5, InfrastructureRating = 4.7, EcologicalRating = 3.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 6, Name = "ЖК Vincent", District = "Вахитовский", Class = "Элит", DeveloperId = 4, 
                MinPrice = 25000000, MaxPrice = 80000000,
                Description = "Клубный дом в историческом центре Казани рядом с парком 'Черное озеро'.",
                ImageUrl = "https://avatars.mds.yandex.net/get-altay/11302718/2a0000018f4ca804b24d30dc496631db4b1e/XXXL",
                CompletionDate = new DateTime(2025, 6, 1),
                DistanceToCenter = 0.5, InfrastructureRating = 5.0, EcologicalRating = 4.2, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 7, Name = "ЖК Легенда", District = "Приволжский", Class = "Комфорт", DeveloperId = 2, 
                MinPrice = 6000000, MaxPrice = 12000000,
                Description = "Яркий жилой комплекс рядом со станцией метро 'Аметьево'.",
                ImageUrl = "https://avatars.mds.yandex.net/get-altay/4465274/2a00000178ee5629109754179e786b4e3808/XXXL",
                CompletionDate = new DateTime(2025, 3, 1),
                DistanceToCenter = 6.0, InfrastructureRating = 4.0, EcologicalRating = 3.0, BuildingMaterial = "Monolith"
            }
        );
    }
}
