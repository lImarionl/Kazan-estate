using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KazanRealEstate.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingMaterial",
                table: "ResidentialComplexes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ResidentialComplexes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "DistanceToCenter",
                table: "ResidentialComplexes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EcologicalRating",
                table: "ResidentialComplexes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ResidentialComplexes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "InfrastructureRating",
                table: "ResidentialComplexes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ResidentialComplexId = table.Column<int>(type: "integer", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_ResidentialComplexes_ResidentialComplexId",
                        column: x => x.ResidentialComplexId,
                        principalTable: "ResidentialComplexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Лидер рынка Татарстана, известный своими проектами 'Царево Village' и 'Весна'.");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Один из старейших застройщиков региона с широким портфелем проектов.");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Застройщик, специализирующийся на современной архитектуре и качественной среде.");

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Description", "Name", "Rating" },
                values: new object[] { 4, "Бутик-застройщик, работающий с историческим центром и элитной недвижимостью.", "КамаСтройИнвест", 4.9000000000000004 });

            migrationBuilder.UpdateData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BuildingMaterial", "CompletionDate", "Description", "DistanceToCenter", "District", "EcologicalRating", "ImageUrl", "InfrastructureRating", "MaxPrice", "MinPrice", "Name" },
                values: new object[] { "Brick", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Уютный пригородный поселок с парками, школами и уникальной атмосферой.", 15.5, "Пестречинский", 4.7999999999999998, "https://images.unsplash.com/photo-1570129477492-45c003edd2be?q=80&w=800", 3.5, 8500000m, 4500000m, "ЖК Царево Village" });

            migrationBuilder.UpdateData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BuildingMaterial", "Class", "CompletionDate", "Description", "DistanceToCenter", "EcologicalRating", "ImageUrl", "InfrastructureRating", "MaxPrice", "MinPrice" },
                values: new object[] { "Brick-Monolith", "Комфорт+", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Большой семейный комплекс с развитой инфраструктурой и закрытыми дворами.", 9.1999999999999993, 4.0, "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?q=80&w=800", 4.2000000000000002, 13000000m, 6500000m });

            migrationBuilder.UpdateData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BuildingMaterial", "CompletionDate", "Description", "DistanceToCenter", "EcologicalRating", "ImageUrl", "InfrastructureRating", "MaxPrice", "MinPrice" },
                values: new object[] { "Monolith", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Современный жилой массив рядом с ТЦ Мега и будущей станцией метро.", 7.5, 3.5, "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?q=80&w=800", 4.5, 16000000m, 7500000m });

            migrationBuilder.InsertData(
                table: "ResidentialComplexes",
                columns: new[] { "Id", "Address", "BuildingMaterial", "Class", "CompletionDate", "Description", "DeveloperId", "DistanceToCenter", "District", "EcologicalRating", "ImageUrl", "InfrastructureRating", "MaxPrice", "MinPrice", "Name" },
                values: new object[,]
                {
                    { 4, "", "Monolith", "Бизнес", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Премиальный комплекс с видом на Казанку и Кремль в самом центре Кварталов.", 3, 3.2000000000000002, "Ново-Савиновский", 3.7999999999999998, "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?q=80&w=800", 4.7999999999999998, 45000000m, 15000000m, "ЖК Savin House" },
                    { 5, "", "Monolith", "Элит", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стеклянные башни на берегу реки с панорамным остеклением и яхт-клубом.", 3, 2.5, "Кировский", 3.5, "https://images.unsplash.com/photo-1475855581690-804d4628733c?q=80&w=800", 4.7000000000000002, 60000000m, 18000000m, "ЖК Atlantis Deluxe" },
                    { 7, "", "Monolith", "Комфорт", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Яркий жилой комплекс рядом со станцией метро 'Аметьево'.", 2, 6.0, "Приволжский", 3.0, "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?q=80&w=800", 4.0, 12000000m, 6000000m, "ЖК Легенда" },
                    { 6, "", "Brick", "Элит", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Клубный дом в историческом центре Казани рядом с парком 'Черное озеро'.", 4, 0.5, "Вахитовский", 4.2000000000000002, "https://images.unsplash.com/photo-1460317442991-0ec23938714b?q=80&w=800", 5.0, 80000000m, 25000000m, "ЖК Vincent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ResidentialComplexId",
                table: "Favorites",
                column: "ResidentialComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DeleteData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "BuildingMaterial",
                table: "ResidentialComplexes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ResidentialComplexes");

            migrationBuilder.DropColumn(
                name: "DistanceToCenter",
                table: "ResidentialComplexes");

            migrationBuilder.DropColumn(
                name: "EcologicalRating",
                table: "ResidentialComplexes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ResidentialComplexes");

            migrationBuilder.DropColumn(
                name: "InfrastructureRating",
                table: "ResidentialComplexes");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Один из крупнейших застройщиков Татарстана.");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Государственный холдинг, строящий современные ЖК.");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Застройщик премиального и комфорт-класса.");

            migrationBuilder.UpdateData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompletionDate", "District", "MaxPrice", "MinPrice", "Name" },
                values: new object[] { null, "Пестречинский/Советский", 8000000m, 4000000m, "ЖК Царево" });

            migrationBuilder.UpdateData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Class", "CompletionDate", "MaxPrice", "MinPrice" },
                values: new object[] { "Комфорт", null, 12000000m, 6000000m });

            migrationBuilder.UpdateData(
                table: "ResidentialComplexes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CompletionDate", "MaxPrice", "MinPrice" },
                values: new object[] { null, 15000000m, 7000000m });
        }
    }
}
