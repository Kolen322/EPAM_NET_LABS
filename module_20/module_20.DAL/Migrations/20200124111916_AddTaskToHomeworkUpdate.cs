using Microsoft.EntityFrameworkCore.Migrations;

namespace module_20.DAL.Migrations
{
    public partial class AddTaskToHomeworkUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "Homeworks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Task",
                value: "L01");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Task",
                value: "L02");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Task",
                value: "L03");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 4,
                column: "Task",
                value: "L04");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 5,
                column: "Task",
                value: "L01");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 6,
                column: "Task",
                value: "L02");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 7,
                column: "Task",
                value: "L03");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 8,
                column: "Task",
                value: "L04");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 9,
                column: "Task",
                value: "L03");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 10,
                column: "Task",
                value: "L04");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 11,
                column: "Task",
                value: "L05");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 12,
                column: "Task",
                value: "L06");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 13,
                column: "Task",
                value: "L03");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 14,
                column: "Task",
                value: "L04");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 15,
                column: "Task",
                value: "L05");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 16,
                column: "Task",
                value: "L06");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 17,
                column: "Task",
                value: "L05");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 18,
                column: "Task",
                value: "L06");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 19,
                column: "Task",
                value: "L07");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 20,
                column: "Task",
                value: "L05");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 21,
                column: "Task",
                value: "L06");

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 22,
                column: "Task",
                value: "L07");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Task",
                table: "Homeworks");
        }
    }
}
