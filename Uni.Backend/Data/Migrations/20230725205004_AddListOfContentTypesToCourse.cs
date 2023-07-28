using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddListOfContentTypesToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseContentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseContentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseContentTypes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CourseContentTypes",
                columns: new[] { "Id", "CourseId", "Name" },
                values: new object[,]
                {
                    { new Guid("90f395b7-aba3-4720-a338-d0260801c185"), null, "Lectures" },
                    { new Guid("a37250cc-47f0-4ac9-9069-9709db0c89e9"), null, "CourseProject" },
                    { new Guid("e6cb3460-fb09-43c1-84b4-3de4542470f5"), null, "LabWorks" },
                    { new Guid("e777af32-eedb-4078-9fdd-2f4d3f489087"), null, "FinalCertification" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseContentTypes_CourseId",
                table: "CourseContentTypes",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseContentTypes");
        }
    }
}
