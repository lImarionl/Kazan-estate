using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KazanRealEstate.Api.Data;
using KazanRealEstate.Api.Repositories;
using KazanRealEstate.Api.Services;
using KazanRealEstate.Api.Services.Identity;
using KazanRealEstate.Api.Repositories.Tables;
using Microsoft.EntityFrameworkCore.Diagnostics;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

// Repositories
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

// Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000") // Default CRA port
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        var unistroyDev = context.Developers.FirstOrDefault(d => d.Name == "Унистрой");
        var akbarsDev = context.Developers.FirstOrDefault(d => d.Name == "Ак Барс Дом");
        var smu88Dev = context.Developers.FirstOrDefault(d => d.Name == "СМУ-88");

        var suvarDev = context.Developers.FirstOrDefault(d => d.Name == "Суварстроит");
        if (suvarDev == null)
        {
            suvarDev = new Developer 
            { 
                Name = "Суварстроит", 
                Rating = 4.6, 
                Description = "Один из крупнейших девелоперов Татарстана, создающий масштабные проекты комфорт- и бизнес-класса." 
            };
            context.Developers.Add(suvarDev);
            context.SaveChanges();
        }

        bool hasNewSeeds = false;

        if (!context.ResidentialComplexes.Any(rc => rc.Name == "ЖК ART City"))
        {
            context.ResidentialComplexes.Add(new ResidentialComplex 
            { 
                Name = "ЖК ART City", 
                District = "Советский", 
                Class = "Комфорт", 
                DeveloperId = unistroyDev?.Id ?? 1, 
                MinPrice = 8500000, 
                MaxPrice = 22000000,
                Description = "Масштабный микрорайон комфорт-класса в Советском районе с концепцией 'двор без машин', собственной пешеходной аллеей, парками, детскими садами и школой.",
                ImageUrl = "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?q=80&w=800",
                CompletionDate = new DateTime(2023, 12, 1),
                DistanceToCenter = 4.5, 
                InfrastructureRating = 4.8, 
                EcologicalRating = 4.0, 
                BuildingMaterial = "Brick-Monolith"
            });
            hasNewSeeds = true;
        }

        if (!context.ResidentialComplexes.Any(rc => rc.Name == "ЖК Яратам"))
        {
            context.ResidentialComplexes.Add(new ResidentialComplex 
            { 
                Name = "ЖК Яратам", 
                District = "Советский", 
                Class = "Комфорт", 
                DeveloperId = smu88Dev?.Id ?? 3, 
                MinPrice = 7000000, 
                MaxPrice = 16000000,
                Description = "Современный жилой комплекс комфорт-класса на улице Бухарская с яркой архитектурой, подземным паркингом и муниципальным детским садом на территории.",
                ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?q=80&w=800",
                CompletionDate = new DateTime(2026, 6, 1),
                DistanceToCenter = 6.8, 
                InfrastructureRating = 4.4, 
                EcologicalRating = 3.8, 
                BuildingMaterial = "Monolith"
            });
            hasNewSeeds = true;
        }

        if (!context.ResidentialComplexes.Any(rc => rc.Name == "ЖК Столичный"))
        {
            context.ResidentialComplexes.Add(new ResidentialComplex 
            { 
                Name = "ЖК Столичный", 
                District = "Ново-Савиновский", 
                Class = "Бизнес", 
                DeveloperId = suvarDev.Id, 
                MinPrice = 10000000, 
                MaxPrice = 35000000,
                Description = "Флагманский комплекс бизнес-класса на улице Чистопольская с парящими мостами-переходами, собственной школой, двухуровневым паркингом и прекрасным видом на Казанку.",
                ImageUrl = "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?q=80&w=800",
                CompletionDate = new DateTime(2022, 6, 1),
                DistanceToCenter = 3.8, 
                InfrastructureRating = 4.9, 
                EcologicalRating = 4.1, 
                BuildingMaterial = "Monolith-Brick"
            });
            hasNewSeeds = true;
        }

        if (!context.ResidentialComplexes.Any(rc => rc.Name == "ЖК Светлая Долина"))
        {
            context.ResidentialComplexes.Add(new ResidentialComplex 
            { 
                Name = "ЖК Светлая Долина", 
                District = "Советский", 
                Class = "Комфорт", 
                DeveloperId = akbarsDev?.Id ?? 2, 
                MinPrice = 6900000, 
                MaxPrice = 12000000,
                Description = "Крупный семейный микрорайон комфорт-класса вдоль Мамадышского тракта с благоустроенной набережной реки Ноксы, парком для прогулок, школами и детскими садами.",
                ImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be?q=80&w=800",
                CompletionDate = new DateTime(2025, 12, 1),
                DistanceToCenter = 11.2, 
                InfrastructureRating = 4.3, 
                EcologicalRating = 4.6, 
                BuildingMaterial = "Panel"
            });
            hasNewSeeds = true;
        }

        if (hasNewSeeds)
        {
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database migration or seeding error");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
