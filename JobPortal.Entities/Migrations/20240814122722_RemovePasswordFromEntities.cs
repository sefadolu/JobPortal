using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Entities.Migrations
{
    /// <inheritdoc />
    public partial class RemovePasswordFromEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "JobSeekers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "JobSeekers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "password123");
        }
    }
}
