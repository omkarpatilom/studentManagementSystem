using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExam.Migrations
{
    public partial class StatusaddedinTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tests",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tests");
        }
    }
}
