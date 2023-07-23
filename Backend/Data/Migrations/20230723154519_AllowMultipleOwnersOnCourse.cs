using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllowMultipleOwnersOnCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_OwnerId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OwnerId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Courses");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourseId",
                table: "Users",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Courses_CourseId",
                table: "Users",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Courses_CourseId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CourseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Courses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OwnerId",
                table: "Courses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_OwnerId",
                table: "Courses",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
