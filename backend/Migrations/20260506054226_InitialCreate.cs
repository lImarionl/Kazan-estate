using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KazanRealEstate.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialComplexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Class = table.Column<string>(type: "text", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    MinPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialComplexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialComplexes_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Description", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Один из крупнейших застройщиков Татарстана.", "Унистрой", 4.7999999999999998 },
                    { 2, "Государственный холдинг, строящий современные ЖК.", "Ак Барс Дом", 4.5 },
                    { 3, "Застройщик премиального и комфорт-класса.", "СМУ-88", 4.7000000000000002 }
                });

            migrationBuilder.InsertData(
                table: "ResidentialComplexes",
                columns: new[] { "Id", "Address", "Class", "CompletionDate", "DeveloperId", "District", "MaxPrice", "MinPrice", "Name" },
                values: new object[,]
                {
                    { 1, "", "Эко-поселок", null, 1, "Пестречинский/Советский", 8000000m, 4000000m, "ЖК Царево" },
                    { 2, "", "Комфорт", null, 1, "Советский", 12000000m, 6000000m, "ЖК Весна" },
                    { 3, "", "Бизнес-лайт", null, 2, "Советский", 15000000m, 7000000m, "ЖК Мой Ритм" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialComplexes_DeveloperId",
                table: "ResidentialComplexes",
                column: "DeveloperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResidentialComplexes");

            migrationBuilder.DropTable(
                name: "Developers");
        }
    }
}
