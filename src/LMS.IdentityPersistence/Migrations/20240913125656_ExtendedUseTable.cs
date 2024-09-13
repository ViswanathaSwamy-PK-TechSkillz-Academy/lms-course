using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e21cef22-a4c2-4549-8c4e-81161a6d3a52", new DateOnly(1980, 1, 1), "Admin", "User", "AQAAAAIAAYagAAAAEKhaNQOtNFLmE0r1RhQLHFA+hFCir3lZ1KnFD87Hp7nC7/UP4nz6sZIKDME5nqbp7g==", "5af46a09-9937-404a-9f40-636ddabae525" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a34ac82-a9ba-4cd7-92a1-46fa51f2e286", "AQAAAAIAAYagAAAAENMHNfy9zYYHC7+2Q7vFtsK4T3on5KO2ys2WWzFngV/dYeT4F6d8t0ZIfhiCZlDaPA==", "711ee4b2-b01b-499b-b9e6-e05ed354a2da" });
        }
    }
}
