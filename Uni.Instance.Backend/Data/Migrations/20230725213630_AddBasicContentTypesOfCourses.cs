using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicContentTypesOfCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsVisibleToStudents = table.Column<bool>(type: "boolean", nullable: false),
                    IconId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileContents_CourseBlocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "CourseBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileContents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileContents_StaticFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "StaticFiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileContents_StaticFiles_IconId",
                        column: x => x.IconId,
                        principalTable: "StaticFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TextContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentId = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsVisibleToStudents = table.Column<bool>(type: "boolean", nullable: false),
                    IconId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextContents_CourseBlocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "CourseBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextContents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextContents_StaticFiles_ContentId",
                        column: x => x.ContentId,
                        principalTable: "StaticFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextContents_StaticFiles_IconId",
                        column: x => x.IconId,
                        principalTable: "StaticFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_BlockId",
                table: "FileContents",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_CourseId",
                table: "FileContents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_FileId",
                table: "FileContents",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_IconId",
                table: "FileContents",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_BlockId",
                table: "TextContents",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_ContentId",
                table: "TextContents",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_CourseId",
                table: "TextContents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_IconId",
                table: "TextContents",
                column: "IconId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileContents");

            migrationBuilder.DropTable(
                name: "TextContents");
        }
    }
}
