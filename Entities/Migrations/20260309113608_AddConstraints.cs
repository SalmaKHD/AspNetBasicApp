using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Coutries",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Coutries",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coutries_CountryID",
                table: "Coutries",
                column: "CountryID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coutries_CountryID",
                table: "Coutries");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Coutries",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coutries",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
