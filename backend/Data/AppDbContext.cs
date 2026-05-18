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
            },
            new ResidentialComplex 
            { 
                Id = 8, Name = "ЖК Яратам", District = "Московский", Class = "Бизнес", DeveloperId = 3, 
                MinPrice = 10900000, MaxPrice = 18300000,
                Description = "ЖК Яратам - отличный выбор в районе Московский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/8/800/600.png",
                CompletionDate = new DateTime(2026, 10, 1),
                DistanceToCenter = 14.1, InfrastructureRating = 4.3, EcologicalRating = 4.6, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 9, Name = "ЖК Светлая долина", District = "Вахитовский", Class = "Бизнес-лайт", DeveloperId = 3, 
                MinPrice = 12600000, MaxPrice = 32100000,
                Description = "ЖК Светлая долина - отличный выбор в районе Вахитовский. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/9/800/600.png",
                CompletionDate = new DateTime(2025, 7, 1),
                DistanceToCenter = 17.3, InfrastructureRating = 4.7, EcologicalRating = 4.3, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 10, Name = "ЖК Лето", District = "Московский", Class = "Бизнес", DeveloperId = 1, 
                MinPrice = 7200000, MaxPrice = 17300000,
                Description = "ЖК Лето - отличный выбор в районе Московский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/10/800/600.png",
                CompletionDate = new DateTime(2024, 3, 1),
                DistanceToCenter = 3.6, InfrastructureRating = 4.3, EcologicalRating = 2.5, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 11, Name = "ЖК Нокса парк", District = "Московский", Class = "Эко-поселок", DeveloperId = 4, 
                MinPrice = 17200000, MaxPrice = 22800000,
                Description = "ЖК Нокса парк - отличный выбор в районе Московский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/11/800/600.png",
                CompletionDate = new DateTime(2024, 10, 1),
                DistanceToCenter = 8.6, InfrastructureRating = 4.8, EcologicalRating = 4.2, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 12, Name = "ЖК Манхэттен", District = "Ново-Савиновский", Class = "Комфорт", DeveloperId = 3, 
                MinPrice = 9300000, MaxPrice = 18200000,
                Description = "ЖК Манхэттен - отличный выбор в районе Ново-Савиновский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/12/800/600.png",
                CompletionDate = new DateTime(2025, 6, 1),
                DistanceToCenter = 12.3, InfrastructureRating = 4.6, EcologicalRating = 3.6, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 13, Name = "ЖК Фермаполис", District = "Авиастроительный", Class = "Бизнес-лайт", DeveloperId = 4, 
                MinPrice = 17600000, MaxPrice = 30300000,
                Description = "ЖК Фермаполис - отличный выбор в районе Авиастроительный. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/13/800/600.png",
                CompletionDate = new DateTime(2027, 1, 1),
                DistanceToCenter = 5.6, InfrastructureRating = 4.8, EcologicalRating = 2.7, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 14, Name = "ЖК Яр Парк", District = "Ново-Савиновский", Class = "Бизнес-лайт", DeveloperId = 3, 
                MinPrice = 18700000, MaxPrice = 21100000,
                Description = "ЖК Яр Парк - отличный выбор в районе Ново-Савиновский. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/14/800/600.png",
                CompletionDate = new DateTime(2025, 7, 1),
                DistanceToCenter = 4.2, InfrastructureRating = 4.9, EcologicalRating = 4.8, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 15, Name = "ЖК Сюита", District = "Приволжский", Class = "Комфорт", DeveloperId = 3, 
                MinPrice = 11500000, MaxPrice = 26500000,
                Description = "ЖК Сюита - отличный выбор в районе Приволжский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/15/800/600.png",
                CompletionDate = new DateTime(2024, 4, 1),
                DistanceToCenter = 6.7, InfrastructureRating = 4.8, EcologicalRating = 2.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 16, Name = "ЖК Столичный", District = "Вахитовский", Class = "Элит", DeveloperId = 2, 
                MinPrice = 19000000, MaxPrice = 37200000,
                Description = "ЖК Столичный - отличный выбор в районе Вахитовский. Класс: Элит.",
                ImageUrl = "https://picsum.photos/seed/16/800/600.png",
                CompletionDate = new DateTime(2024, 4, 1),
                DistanceToCenter = 1.5, InfrastructureRating = 4.7, EcologicalRating = 3.2, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 17, Name = "ЖК UNO", District = "Советский", Class = "Комфорт", DeveloperId = 3, 
                MinPrice = 9800000, MaxPrice = 32500000,
                Description = "ЖК UNO - отличный выбор в районе Советский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/17/800/600.png",
                CompletionDate = new DateTime(2024, 6, 1),
                DistanceToCenter = 11.0, InfrastructureRating = 2.7, EcologicalRating = 3.9, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 18, Name = "ЖК Оазис", District = "Ново-Савиновский", Class = "Эко-поселок", DeveloperId = 4, 
                MinPrice = 10700000, MaxPrice = 18400000,
                Description = "ЖК Оазис - отличный выбор в районе Ново-Савиновский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/18/800/600.png",
                CompletionDate = new DateTime(2026, 5, 1),
                DistanceToCenter = 11.1, InfrastructureRating = 3.0, EcologicalRating = 3.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 19, Name = "ЖК Горизонт", District = "Московский", Class = "Эко-поселок", DeveloperId = 3, 
                MinPrice = 9200000, MaxPrice = 19900000,
                Description = "ЖК Горизонт - отличный выбор в районе Московский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/19/800/600.png",
                CompletionDate = new DateTime(2024, 3, 1),
                DistanceToCenter = 11.4, InfrastructureRating = 3.6, EcologicalRating = 3.2, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 20, Name = "ЖК Триумф", District = "Ново-Савиновский", Class = "Комфорт+", DeveloperId = 2, 
                MinPrice = 7800000, MaxPrice = 28000000,
                Description = "ЖК Триумф - отличный выбор в районе Ново-Савиновский. Класс: Комфорт+.",
                ImageUrl = "https://picsum.photos/seed/20/800/600.png",
                CompletionDate = new DateTime(2025, 9, 1),
                DistanceToCenter = 2.1, InfrastructureRating = 3.4, EcologicalRating = 4.5, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 21, Name = "ЖК Аврора", District = "Московский", Class = "Эко-поселок", DeveloperId = 3, 
                MinPrice = 7800000, MaxPrice = 23400000,
                Description = "ЖК Аврора - отличный выбор в районе Московский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/21/800/600.png",
                CompletionDate = new DateTime(2026, 1, 1),
                DistanceToCenter = 19.8, InfrastructureRating = 4.6, EcologicalRating = 3.4, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 22, Name = "ЖК Гармония", District = "Советский", Class = "Бизнес", DeveloperId = 1, 
                MinPrice = 9400000, MaxPrice = 28600000,
                Description = "ЖК Гармония - отличный выбор в районе Советский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/22/800/600.png",
                CompletionDate = new DateTime(2025, 8, 1),
                DistanceToCenter = 6.8, InfrastructureRating = 4.3, EcologicalRating = 2.6, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 23, Name = "ЖК Созвездие", District = "Пестречинский", Class = "Комфорт", DeveloperId = 1, 
                MinPrice = 14400000, MaxPrice = 19200000,
                Description = "ЖК Созвездие - отличный выбор в районе Пестречинский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/23/800/600.png",
                CompletionDate = new DateTime(2025, 7, 1),
                DistanceToCenter = 18.4, InfrastructureRating = 2.6, EcologicalRating = 4.6, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 24, Name = "ЖК Эмеральд", District = "Ново-Савиновский", Class = "Бизнес-лайт", DeveloperId = 1, 
                MinPrice = 16700000, MaxPrice = 45100000,
                Description = "ЖК Эмеральд - отличный выбор в районе Ново-Савиновский. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/24/800/600.png",
                CompletionDate = new DateTime(2027, 4, 1),
                DistanceToCenter = 18.5, InfrastructureRating = 4.4, EcologicalRating = 4.0, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 25, Name = "ЖК Ривьера", District = "Авиастроительный", Class = "Эко-поселок", DeveloperId = 1, 
                MinPrice = 15500000, MaxPrice = 34700000,
                Description = "ЖК Ривьера - отличный выбор в районе Авиастроительный. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/25/800/600.png",
                CompletionDate = new DateTime(2024, 7, 1),
                DistanceToCenter = 2.3, InfrastructureRating = 4.0, EcologicalRating = 3.6, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 26, Name = "ЖК Престиж", District = "Советский", Class = "Комфорт+", DeveloperId = 2, 
                MinPrice = 10600000, MaxPrice = 33600000,
                Description = "ЖК Престиж - отличный выбор в районе Советский. Класс: Комфорт+.",
                ImageUrl = "https://picsum.photos/seed/26/800/600.png",
                CompletionDate = new DateTime(2024, 2, 1),
                DistanceToCenter = 19.6, InfrastructureRating = 4.7, EcologicalRating = 3.1, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 27, Name = "ЖК Аквамарин", District = "Приволжский", Class = "Комфорт", DeveloperId = 4, 
                MinPrice = 17000000, MaxPrice = 35300000,
                Description = "ЖК Аквамарин - отличный выбор в районе Приволжский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/27/800/600.png",
                CompletionDate = new DateTime(2024, 6, 1),
                DistanceToCenter = 3.4, InfrastructureRating = 3.1, EcologicalRating = 4.9, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 28, Name = "ЖК Нова", District = "Авиастроительный", Class = "Бизнес-лайт", DeveloperId = 4, 
                MinPrice = 8000000, MaxPrice = 23300000,
                Description = "ЖК Нова - отличный выбор в районе Авиастроительный. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/28/800/600.png",
                CompletionDate = new DateTime(2024, 10, 1),
                DistanceToCenter = 0.9, InfrastructureRating = 2.9, EcologicalRating = 4.0, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 29, Name = "ЖК Империя", District = "Авиастроительный", Class = "Элит", DeveloperId = 3, 
                MinPrice = 11200000, MaxPrice = 22400000,
                Description = "ЖК Империя - отличный выбор в районе Авиастроительный. Класс: Элит.",
                ImageUrl = "https://picsum.photos/seed/29/800/600.png",
                CompletionDate = new DateTime(2026, 3, 1),
                DistanceToCenter = 5.8, InfrastructureRating = 4.7, EcologicalRating = 3.8, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 30, Name = "ЖК Панорама", District = "Кировский", Class = "Бизнес", DeveloperId = 2, 
                MinPrice = 7900000, MaxPrice = 29900000,
                Description = "ЖК Панорама - отличный выбор в районе Кировский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/30/800/600.png",
                CompletionDate = new DateTime(2025, 1, 1),
                DistanceToCenter = 13.4, InfrastructureRating = 4.0, EcologicalRating = 2.7, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 31, Name = "ЖК Виктория", District = "Вахитовский", Class = "Бизнес", DeveloperId = 2, 
                MinPrice = 5500000, MaxPrice = 34400000,
                Description = "ЖК Виктория - отличный выбор в районе Вахитовский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/31/800/600.png",
                CompletionDate = new DateTime(2026, 5, 1),
                DistanceToCenter = 2.5, InfrastructureRating = 4.9, EcologicalRating = 5.0, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 32, Name = "ЖК Мегаполис", District = "Кировский", Class = "Бизнес", DeveloperId = 4, 
                MinPrice = 18200000, MaxPrice = 47700000,
                Description = "ЖК Мегаполис - отличный выбор в районе Кировский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/32/800/600.png",
                CompletionDate = new DateTime(2026, 3, 1),
                DistanceToCenter = 13.2, InfrastructureRating = 4.1, EcologicalRating = 2.9, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 33, Name = "ЖК Изумрудный", District = "Ново-Савиновский", Class = "Бизнес", DeveloperId = 2, 
                MinPrice = 17400000, MaxPrice = 32700000,
                Description = "ЖК Изумрудный - отличный выбор в районе Ново-Савиновский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/33/800/600.png",
                CompletionDate = new DateTime(2024, 9, 1),
                DistanceToCenter = 15.9, InfrastructureRating = 3.4, EcologicalRating = 2.6, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 34, Name = "ЖК Северная Звезда", District = "Авиастроительный", Class = "Комфорт", DeveloperId = 2, 
                MinPrice = 17500000, MaxPrice = 34600000,
                Description = "ЖК Северная Звезда - отличный выбор в районе Авиастроительный. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/34/800/600.png",
                CompletionDate = new DateTime(2024, 11, 1),
                DistanceToCenter = 16.6, InfrastructureRating = 4.7, EcologicalRating = 4.7, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 35, Name = "ЖК Южный Парк", District = "Авиастроительный", Class = "Эко-поселок", DeveloperId = 2, 
                MinPrice = 14300000, MaxPrice = 29800000,
                Description = "ЖК Южный Парк - отличный выбор в районе Авиастроительный. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/35/800/600.png",
                CompletionDate = new DateTime(2026, 8, 1),
                DistanceToCenter = 1.4, InfrastructureRating = 2.7, EcologicalRating = 3.5, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 36, Name = "ЖК Западный", District = "Вахитовский", Class = "Бизнес", DeveloperId = 1, 
                MinPrice = 14700000, MaxPrice = 27600000,
                Description = "ЖК Западный - отличный выбор в районе Вахитовский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/36/800/600.png",
                CompletionDate = new DateTime(2024, 5, 1),
                DistanceToCenter = 18.8, InfrastructureRating = 3.2, EcologicalRating = 3.8, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 37, Name = "ЖК Восточный", District = "Ново-Савиновский", Class = "Бизнес", DeveloperId = 2, 
                MinPrice = 19900000, MaxPrice = 30800000,
                Description = "ЖК Восточный - отличный выбор в районе Ново-Савиновский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/37/800/600.png",
                CompletionDate = new DateTime(2024, 12, 1),
                DistanceToCenter = 1.6, InfrastructureRating = 3.7, EcologicalRating = 2.8, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 38, Name = "ЖК Казанские Зори", District = "Ново-Савиновский", Class = "Элит", DeveloperId = 4, 
                MinPrice = 9100000, MaxPrice = 35900000,
                Description = "ЖК Казанские Зори - отличный выбор в районе Ново-Савиновский. Класс: Элит.",
                ImageUrl = "https://picsum.photos/seed/38/800/600.png",
                CompletionDate = new DateTime(2027, 1, 1),
                DistanceToCenter = 4.9, InfrastructureRating = 3.8, EcologicalRating = 4.4, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 39, Name = "ЖК Серебряный берег", District = "Пестречинский", Class = "Элит", DeveloperId = 4, 
                MinPrice = 19800000, MaxPrice = 30700000,
                Description = "ЖК Серебряный берег - отличный выбор в районе Пестречинский. Класс: Элит.",
                ImageUrl = "https://picsum.photos/seed/39/800/600.png",
                CompletionDate = new DateTime(2024, 8, 1),
                DistanceToCenter = 8.3, InfrastructureRating = 2.5, EcologicalRating = 2.5, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 40, Name = "ЖК Золотые ключи", District = "Приволжский", Class = "Эко-поселок", DeveloperId = 3, 
                MinPrice = 16800000, MaxPrice = 31000000,
                Description = "ЖК Золотые ключи - отличный выбор в районе Приволжский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/40/800/600.png",
                CompletionDate = new DateTime(2026, 3, 1),
                DistanceToCenter = 14.5, InfrastructureRating = 3.5, EcologicalRating = 4.4, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 41, Name = "ЖК Тихая гавань", District = "Кировский", Class = "Эко-поселок", DeveloperId = 4, 
                MinPrice = 9000000, MaxPrice = 30800000,
                Description = "ЖК Тихая гавань - отличный выбор в районе Кировский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/41/800/600.png",
                CompletionDate = new DateTime(2027, 5, 1),
                DistanceToCenter = 2.1, InfrastructureRating = 4.2, EcologicalRating = 3.1, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 42, Name = "ЖК Солнечный", District = "Приволжский", Class = "Комфорт+", DeveloperId = 1, 
                MinPrice = 12000000, MaxPrice = 39900000,
                Description = "ЖК Солнечный - отличный выбор в районе Приволжский. Класс: Комфорт+.",
                ImageUrl = "https://picsum.photos/seed/42/800/600.png",
                CompletionDate = new DateTime(2025, 7, 1),
                DistanceToCenter = 2.1, InfrastructureRating = 4.0, EcologicalRating = 2.7, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 43, Name = "ЖК Зеленый квартал", District = "Московский", Class = "Комфорт+", DeveloperId = 2, 
                MinPrice = 8700000, MaxPrice = 19300000,
                Description = "ЖК Зеленый квартал - отличный выбор в районе Московский. Класс: Комфорт+.",
                ImageUrl = "https://picsum.photos/seed/43/800/600.png",
                CompletionDate = new DateTime(2025, 3, 1),
                DistanceToCenter = 8.5, InfrastructureRating = 2.8, EcologicalRating = 4.5, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 44, Name = "ЖК Жемчужина", District = "Авиастроительный", Class = "Комфорт", DeveloperId = 1, 
                MinPrice = 7400000, MaxPrice = 32800000,
                Description = "ЖК Жемчужина - отличный выбор в районе Авиастроительный. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/44/800/600.png",
                CompletionDate = new DateTime(2024, 9, 1),
                DistanceToCenter = 8.9, InfrastructureRating = 3.5, EcologicalRating = 2.6, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 45, Name = "ЖК Премьер", District = "Авиастроительный", Class = "Комфорт", DeveloperId = 4, 
                MinPrice = 16000000, MaxPrice = 25800000,
                Description = "ЖК Премьер - отличный выбор в районе Авиастроительный. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/45/800/600.png",
                CompletionDate = new DateTime(2026, 3, 1),
                DistanceToCenter = 15.2, InfrastructureRating = 3.9, EcologicalRating = 3.7, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 46, Name = "ЖК Авангард", District = "Советский", Class = "Комфорт+", DeveloperId = 4, 
                MinPrice = 18900000, MaxPrice = 42300000,
                Description = "ЖК Авангард - отличный выбор в районе Советский. Класс: Комфорт+.",
                ImageUrl = "https://picsum.photos/seed/46/800/600.png",
                CompletionDate = new DateTime(2024, 6, 1),
                DistanceToCenter = 10.1, InfrastructureRating = 4.4, EcologicalRating = 3.5, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 47, Name = "ЖК Модерн", District = "Авиастроительный", Class = "Бизнес", DeveloperId = 1, 
                MinPrice = 13400000, MaxPrice = 42200000,
                Description = "ЖК Модерн - отличный выбор в районе Авиастроительный. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/47/800/600.png",
                CompletionDate = new DateTime(2026, 11, 1),
                DistanceToCenter = 14.4, InfrastructureRating = 3.3, EcologicalRating = 4.1, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 48, Name = "ЖК Лофт", District = "Ново-Савиновский", Class = "Эко-поселок", DeveloperId = 1, 
                MinPrice = 13700000, MaxPrice = 19200000,
                Description = "ЖК Лофт - отличный выбор в районе Ново-Савиновский. Класс: Эко-поселок.",
                ImageUrl = "https://picsum.photos/seed/48/800/600.png",
                CompletionDate = new DateTime(2026, 4, 1),
                DistanceToCenter = 14.7, InfrastructureRating = 3.5, EcologicalRating = 3.9, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 49, Name = "ЖК Классика", District = "Ново-Савиновский", Class = "Комфорт", DeveloperId = 4, 
                MinPrice = 15900000, MaxPrice = 31000000,
                Description = "ЖК Классика - отличный выбор в районе Ново-Савиновский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/49/800/600.png",
                CompletionDate = new DateTime(2027, 4, 1),
                DistanceToCenter = 5.4, InfrastructureRating = 5.0, EcologicalRating = 3.6, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 50, Name = "ЖК Ренессанс", District = "Советский", Class = "Комфорт", DeveloperId = 2, 
                MinPrice = 17600000, MaxPrice = 32000000,
                Description = "ЖК Ренессанс - отличный выбор в районе Советский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/50/800/600.png",
                CompletionDate = new DateTime(2024, 1, 1),
                DistanceToCenter = 12.0, InfrastructureRating = 4.1, EcologicalRating = 2.7, BuildingMaterial = "Brick-Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 51, Name = "ЖК Эклипс", District = "Ново-Савиновский", Class = "Комфорт", DeveloperId = 2, 
                MinPrice = 18300000, MaxPrice = 31000000,
                Description = "ЖК Эклипс - отличный выбор в районе Ново-Савиновский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/51/800/600.png",
                CompletionDate = new DateTime(2026, 5, 1),
                DistanceToCenter = 4.7, InfrastructureRating = 4.6, EcologicalRating = 3.2, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 52, Name = "ЖК Олимп", District = "Кировский", Class = "Бизнес", DeveloperId = 3, 
                MinPrice = 12000000, MaxPrice = 23700000,
                Description = "ЖК Олимп - отличный выбор в районе Кировский. Класс: Бизнес.",
                ImageUrl = "https://picsum.photos/seed/52/800/600.png",
                CompletionDate = new DateTime(2027, 3, 1),
                DistanceToCenter = 0.9, InfrastructureRating = 3.4, EcologicalRating = 4.6, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 53, Name = "ЖК Аристократ", District = "Авиастроительный", Class = "Комфорт+", DeveloperId = 1, 
                MinPrice = 17900000, MaxPrice = 25000000,
                Description = "ЖК Аристократ - отличный выбор в районе Авиастроительный. Класс: Комфорт+.",
                ImageUrl = "https://picsum.photos/seed/53/800/600.png",
                CompletionDate = new DateTime(2026, 6, 1),
                DistanceToCenter = 2.0, InfrastructureRating = 2.6, EcologicalRating = 3.7, BuildingMaterial = "Monolith"
            },
            new ResidentialComplex 
            { 
                Id = 54, Name = "ЖК Корона", District = "Кировский", Class = "Бизнес-лайт", DeveloperId = 2, 
                MinPrice = 5300000, MaxPrice = 18000000,
                Description = "ЖК Корона - отличный выбор в районе Кировский. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/54/800/600.png",
                CompletionDate = new DateTime(2024, 7, 1),
                DistanceToCenter = 17.6, InfrastructureRating = 3.4, EcologicalRating = 3.8, BuildingMaterial = "Panel"
            },
            new ResidentialComplex 
            { 
                Id = 55, Name = "ЖК Эдельвейс", District = "Авиастроительный", Class = "Бизнес-лайт", DeveloperId = 1, 
                MinPrice = 9400000, MaxPrice = 38700000,
                Description = "ЖК Эдельвейс - отличный выбор в районе Авиастроительный. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/55/800/600.png",
                CompletionDate = new DateTime(2024, 3, 1),
                DistanceToCenter = 6.8, InfrastructureRating = 2.6, EcologicalRating = 4.4, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 56, Name = "ЖК Маэстро", District = "Авиастроительный", Class = "Бизнес-лайт", DeveloperId = 4, 
                MinPrice = 14400000, MaxPrice = 43600000,
                Description = "ЖК Маэстро - отличный выбор в районе Авиастроительный. Класс: Бизнес-лайт.",
                ImageUrl = "https://picsum.photos/seed/56/800/600.png",
                CompletionDate = new DateTime(2025, 4, 1),
                DistanceToCenter = 12.1, InfrastructureRating = 4.6, EcologicalRating = 4.3, BuildingMaterial = "Brick"
            },
            new ResidentialComplex 
            { 
                Id = 57, Name = "ЖК Квартет", District = "Ново-Савиновский", Class = "Комфорт", DeveloperId = 2, 
                MinPrice = 15800000, MaxPrice = 18400000,
                Description = "ЖК Квартет - отличный выбор в районе Ново-Савиновский. Класс: Комфорт.",
                ImageUrl = "https://picsum.photos/seed/57/800/600.png",
                CompletionDate = new DateTime(2025, 8, 1),
                DistanceToCenter = 17.6, InfrastructureRating = 3.7, EcologicalRating = 2.7, BuildingMaterial = "Brick"
            }
        );
    }
}
