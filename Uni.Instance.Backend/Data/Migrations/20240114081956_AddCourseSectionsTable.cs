using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSectionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSections", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CourseSections",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("33fe1815-0e3b-4991-8add-c163a2286711"), "Зачёт" },
                    { new Guid("965d07a0-8666-49a9-af0e-797944c65f5f"), "Лабораторные работы" },
                    { new Guid("a19c6184-46c3-40fa-bef7-32ab966a4946"), "Лекции" },
                    { new Guid("adb191be-1e90-46e2-9886-1bfe911a0d66"), "Самостоятельная работа студентов" },
                    { new Guid("d10a8149-afb3-416b-8bfe-3dbbedbec517"), "Курсовой проект" },
                    { new Guid("d2604c2a-550a-4f7e-8da8-1864e6337377"), "Экзамен" },
                    { new Guid("fa013e01-8a58-4ba5-b426-72b790c3ba4f"), "Курсовая работа" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSections");
        }
    }
}
