using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aip.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStaticFileAndDescriptionToAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_FileId",
                table: "Assignments",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_StaticFiles_FileId",
                table: "Assignments",
                column: "FileId",
                principalTable: "StaticFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_StaticFiles_FileId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_FileId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Assignments");
        }
    }
}
