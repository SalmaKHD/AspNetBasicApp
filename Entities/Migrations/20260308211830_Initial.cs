using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coutries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coutries", x => x.CountryID);
                });

            migrationBuilder.InsertData(
                table: "Coutries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { new Guid("89d1e09d-e685-4f88-acdb-7df862831e8c"), "Brazil" },
                    { new Guid("a2914ee4-c8f6-4b91-9e2b-dc6da114d0f9"), "Canada" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coutries");
        }
    }
}
