using Microsoft.EntityFrameworkCore.Migrations;

namespace module_20.DAL.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_Id",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_Id",
                table: "Lectures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_Id",
                table: "Lecturers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_Id",
                table: "Homeworks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Id",
                table: "Courses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_Id",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_Id",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_Id",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Id",
                table: "Courses");
        }
    }
}
