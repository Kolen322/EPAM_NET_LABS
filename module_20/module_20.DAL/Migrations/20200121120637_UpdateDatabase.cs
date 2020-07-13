using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace module_20.DAL.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Attendance",
                table: "StudentLecture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "ivanov@epam.com", "+7 (931) 945-23-45" });

            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "korobov@epam.com", "+7 (931) 955-23-45" });

            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "burmistrov@epam.com", "+7 (931) 235-23-45" });

            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "slavnov@epam.com", "+7 (931) 967-23-45" });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CourseId", "Date", "Name" },
                values: new object[,]
                {
                    { 7, 4, new DateTime(2019, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Present Simple" },
                    { 1, 1, new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "How to find a derivative?" },
                    { 5, 3, new DateTime(2019, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to the Assembler Language" },
                    { 4, 2, new DateTime(2019, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Creating types in C#" },
                    { 3, 2, new DateTime(2019, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to the C# Language" },
                    { 2, 1, new DateTime(2019, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Logarithmic Derivative" },
                    { 6, 3, new DateTime(2019, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "What is Register?" }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "kolen322@yandex.ru", "+7 (931) 945-23-45" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "khebnev@mail.ru", "+7 (931) 965-23-45" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "medvedevI@mail.ru", "+7 (931) 949-23-45" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "ivanutinA@mail.ru", "+7 (931) 915-23-45" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "malininV@mail.ru", "+7 (931) 945-63-45" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "Mobile" },
                values: new object[] { "suetovaE@mail.ru", "+7 (931) 948-23-45" });

            migrationBuilder.InsertData(
                table: "Homeworks",
                columns: new[] { "Id", "LectureId", "Mark", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, 5, 1 },
                    { 17, 5, 5, 5 },
                    { 15, 5, 0, 4 },
                    { 11, 5, 5, 3 },
                    { 12, 6, 5, 3 },
                    { 16, 6, 0, 4 },
                    { 14, 4, 0, 4 },
                    { 10, 4, 5, 3 },
                    { 8, 4, 0, 2 },
                    { 4, 4, 4, 1 },
                    { 18, 6, 5, 5 },
                    { 20, 5, 5, 6 },
                    { 21, 6, 5, 6 },
                    { 13, 3, 0, 4 },
                    { 9, 3, 5, 3 },
                    { 7, 3, 0, 2 },
                    { 3, 3, 4, 1 },
                    { 6, 2, 0, 2 },
                    { 2, 2, 5, 1 },
                    { 19, 7, 5, 5 },
                    { 22, 7, 5, 6 },
                    { 5, 1, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "StudentLecture",
                columns: new[] { "StudentId", "LectureId", "Attendance" },
                values: new object[,]
                {
                    { 3, 6, true },
                    { 5, 6, true },
                    { 6, 6, true },
                    { 6, 5, true },
                    { 5, 5, true },
                    { 4, 6, true },
                    { 4, 5, true },
                    { 2, 4, false },
                    { 4, 4, true },
                    { 3, 4, true },
                    { 5, 7, true },
                    { 1, 4, true },
                    { 4, 3, true },
                    { 3, 3, true },
                    { 2, 3, false },
                    { 1, 3, true },
                    { 2, 2, false },
                    { 1, 2, true },
                    { 2, 1, false },
                    { 1, 1, true },
                    { 3, 5, true },
                    { 6, 7, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "StudentLecture",
                keyColumns: new[] { "StudentId", "LectureId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "StudentLecture");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Lecturers");
        }
    }
}
