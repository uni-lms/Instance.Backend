using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyToCourseAndBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseBlocks_Courses_CourseId",
                table: "CourseBlocks");

            migrationBuilder.DropIndex(
                name: "IX_CourseBlocks_CourseId",
                table: "CourseBlocks");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseBlocks");

            migrationBuilder.CreateTable(
                name: "CourseCourseBlock",
                columns: table => new
                {
                    BlocksId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseBlock", x => new { x.BlocksId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CourseCourseBlock_CourseBlocks_BlocksId",
                        column: x => x.BlocksId,
                        principalTable: "CourseBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseBlock_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseBlock_CoursesId",
                table: "CourseCourseBlock",
                column: "CoursesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseBlock");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "CourseBlocks",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CourseBlocks",
                keyColumn: "Id",
                keyValue: new Guid("90f395b7-aba3-4720-a338-d0260801c185"),
                column: "CourseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseBlocks",
                keyColumn: "Id",
                keyValue: new Guid("a37250cc-47f0-4ac9-9069-9709db0c89e9"),
                column: "CourseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseBlocks",
                keyColumn: "Id",
                keyValue: new Guid("e6cb3460-fb09-43c1-84b4-3de4542470f5"),
                column: "CourseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseBlocks",
                keyColumn: "Id",
                keyValue: new Guid("e777af32-eedb-4078-9fdd-2f4d3f489087"),
                column: "CourseId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CourseBlocks_CourseId",
                table: "CourseBlocks",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseBlocks_Courses_CourseId",
                table: "CourseBlocks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
