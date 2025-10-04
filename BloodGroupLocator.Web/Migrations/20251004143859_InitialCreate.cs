using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodGroupLocator.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "BloodGroup", "CreatedAt", "Email", "Latitude", "Longitude", "Name", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "123 Main St, New York, NY 10001", "O+", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@email.com", 40.712800000000001, -74.006, "John Doe", "+1-555-0123", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "456 Oak Ave, Los Angeles, CA 90210", "A+", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@email.com", 34.052199999999999, -118.2437, "Jane Smith", "+1-555-0456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "789 Pine St, Chicago, IL 60601", "B+", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike.johnson@email.com", 41.878100000000003, -87.629800000000003, "Mike Johnson", "+1-555-0789", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
