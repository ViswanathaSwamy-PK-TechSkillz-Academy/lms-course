using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTheSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lms");

            migrationBuilder.RenameTable(
                name: "LeaveTypes",
                newName: "LeaveTypes",
                newSchema: "lms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "LeaveTypes",
                schema: "lms",
                newName: "LeaveTypes");
        }
    }
}
