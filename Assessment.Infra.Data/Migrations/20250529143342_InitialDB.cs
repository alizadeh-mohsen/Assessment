using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Assessment.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    JobPosition = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicHolidays", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "JobPosition", "Name" },
                values: new object[] { 1, "M.Alizadeh.Net@outlook.com", "Full Stack .NET Developer", "Mohsen Alizadeh" });

            migrationBuilder.InsertData(
                table: "PublicHolidays",
                columns: new[] { "Id", "Date", "Description" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Year’s Day" },
                    { 2, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good Friday" },
                    { 3, new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Easter Monday" },
                    { 4, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Early May Bank Holiday" },
                    { 5, new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spring Bank Holiday" },
                    { 6, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer Bank Holiday" },
                    { 7, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christmas Day" },
                    { 8, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Boxing Day" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PublicHolidays");
        }
    }
}
