using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHouse.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemperatureCelsius = table.Column<float>(nullable: false),
                    TemperatureFahrenheit = table.Column<float>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    HeatIndex = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temperature");
        }
    }
}
