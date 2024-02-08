using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Instance.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFileContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaticFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Checksum = table.Column<string>(type: "text", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    Filepath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AvailableUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AvailableSince = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileContents_CourseSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "CourseSections",
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_CourseId",
                table: "FileContents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_FileId",
                table: "FileContents",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_SectionId",
                table: "FileContents",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileContents");

            migrationBuilder.DropTable(
                name: "StaticFiles");
        }
    }
}
