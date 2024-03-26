using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aip.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTextContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    InternshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsVisibleToStudents = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextContents_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextContents_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_InternshipId",
                table: "TextContents",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_SectionId",
                table: "TextContents",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextContents");
        }
    }
}
