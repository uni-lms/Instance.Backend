using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIconFromContentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContents_StaticFiles_IconId",
                table: "FileContents");

            migrationBuilder.DropForeignKey(
                name: "FK_TextContents_StaticFiles_IconId",
                table: "TextContents");

            migrationBuilder.DropIndex(
                name: "IX_TextContents_IconId",
                table: "TextContents");

            migrationBuilder.DropIndex(
                name: "IX_FileContents_IconId",
                table: "FileContents");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "TextContents");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "FileContents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconId",
                table: "TextContents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconId",
                table: "FileContents",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_IconId",
                table: "TextContents",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContents_IconId",
                table: "FileContents",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContents_StaticFiles_IconId",
                table: "FileContents",
                column: "IconId",
                principalTable: "StaticFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextContents_StaticFiles_IconId",
                table: "TextContents",
                column: "IconId",
                principalTable: "StaticFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
