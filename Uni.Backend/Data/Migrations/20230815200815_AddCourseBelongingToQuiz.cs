using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseBelongingToQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseBlockId",
                table: "QuizContents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "QuizContents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_QuizContents_CourseBlockId",
                table: "QuizContents",
                column: "CourseBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizContents_CourseId",
                table: "QuizContents",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizContents_CourseBlocks_CourseBlockId",
                table: "QuizContents",
                column: "CourseBlockId",
                principalTable: "CourseBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizContents_Courses_CourseId",
                table: "QuizContents",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizContents_CourseBlocks_CourseBlockId",
                table: "QuizContents");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizContents_Courses_CourseId",
                table: "QuizContents");

            migrationBuilder.DropIndex(
                name: "IX_QuizContents_CourseBlockId",
                table: "QuizContents");

            migrationBuilder.DropIndex(
                name: "IX_QuizContents_CourseId",
                table: "QuizContents");

            migrationBuilder.DropColumn(
                name: "CourseBlockId",
                table: "QuizContents");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "QuizContents");
        }
    }
}
