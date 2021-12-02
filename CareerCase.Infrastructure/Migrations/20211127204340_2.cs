using Microsoft.EntityFrameworkCore.Migrations;

namespace CareerCase.Infrastructure.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Jobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Phone",
                table: "Companies",
                column: "Phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_Phone",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Jobs");
        }
    }
}
