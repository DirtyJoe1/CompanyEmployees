using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Age", "GradeId", "Name" },
                values: new object[,]
                {
                    { new Guid("49258e36-100f-4bdb-9cd0-cb5db876e903"), 18, new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"), "Pivkin Aleksey" },
                    { new Guid("ac963d4c-0383-4f52-9a64-2132be68b39b"), 18, new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"), "Kanaykin Aleksandr" },
                    { new Guid("cf757914-e4ef-4515-a5c2-ec0b3744ffc2"), 19, new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"), "Ovtin Ruslan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: new Guid("49258e36-100f-4bdb-9cd0-cb5db876e903"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: new Guid("ac963d4c-0383-4f52-9a64-2132be68b39b"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: new Guid("cf757914-e4ef-4515-a5c2-ec0b3744ffc2"));
        }
    }
}
