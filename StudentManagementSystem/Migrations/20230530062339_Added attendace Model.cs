using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedattendaceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollDetails_facultyDetails_FacultyDetailsFacultyId",
                table: "EnrollDetails");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyDetailsFacultyId",
                table: "EnrollDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => x.id);
                    table.ForeignKey(
                        name: "FK_attendance_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "RollNo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_StudentId",
                table: "attendance",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollDetails_facultyDetails_FacultyDetailsFacultyId",
                table: "EnrollDetails",
                column: "FacultyDetailsFacultyId",
                principalTable: "facultyDetails",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollDetails_facultyDetails_FacultyDetailsFacultyId",
                table: "EnrollDetails");

            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyDetailsFacultyId",
                table: "EnrollDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollDetails_facultyDetails_FacultyDetailsFacultyId",
                table: "EnrollDetails",
                column: "FacultyDetailsFacultyId",
                principalTable: "facultyDetails",
                principalColumn: "FacultyId");
        }
    }
}
