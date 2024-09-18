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
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "646a8e9b-69f0-4c3b-9740-6ac2b4bf76b0", "AQAAAAIAAYagAAAAEIkvMnhbBLjPJwKnIGe7/fRe8QUufd86LZ1GA00yBJSwi4G2+ERiZsyQ11E1nSxZ2Q==", "8f88513c-575e-40d1-ae30-844556a9fb2f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e21cef22-a4c2-4549-8c4e-81161a6d3a52", "AQAAAAIAAYagAAAAEKhaNQOtNFLmE0r1RhQLHFA+hFCir3lZ1KnFD87Hp7nC7/UP4nz6sZIKDME5nqbp7g==", "5af46a09-9937-404a-9f40-636ddabae525" });
        }
    }
}
