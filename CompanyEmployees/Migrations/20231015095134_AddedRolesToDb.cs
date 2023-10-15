using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c689ecf-71b1-432d-a6f8-ec628f6e26c3", "42111dd9-8055-49b3-9d7b-c78af18111f9", "Manager", "MANAGER" },
                    { "52aabf07-eb79-40fb-abc6-cd564a2e329b", "46518cd2-22a5-4243-abfb-d9259e18513a", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c689ecf-71b1-432d-a6f8-ec628f6e26c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52aabf07-eb79-40fb-abc6-cd564a2e329b");
        }
    }
}
