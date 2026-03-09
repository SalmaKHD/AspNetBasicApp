using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class CountriesStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetCountries = @"
CREATE PROCEDURE [dbo].[GetCountries]
AS BEGIN
    SELECT Name FROM [dbo].[Coutries]
END
";
   migrationBuilder.Sql(sp_GetCountries);     

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetCountries = @"
DROP PROCEDURE [dbo].[GetCountries]
AS BEGIN
    SELECT Name FROM [dbo].[Coutries]
END
";
            migrationBuilder.Sql(sp_GetCountries);
        }
    }
}
