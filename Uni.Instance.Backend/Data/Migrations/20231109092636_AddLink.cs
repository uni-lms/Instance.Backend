using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionChoices_AccruedPoints_AccruedPointId",
                table: "QuestionChoices");

            migrationBuilder.DropIndex(
                name: "IX_QuestionChoices_AccruedPointId",
                table: "QuestionChoices");

            migrationBuilder.DropColumn(
                name: "AccruedPointId",
                table: "QuestionChoices");

            migrationBuilder.CreateTable(
                name: "AccruedPointQuestionChoice",
                columns: table => new
                {
                    AccruedPointsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedChoicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccruedPointQuestionChoice", x => new { x.AccruedPointsId, x.SelectedChoicesId });
                    table.ForeignKey(
                        name: "FK_AccruedPointQuestionChoice_AccruedPoints_AccruedPointsId",
                        column: x => x.AccruedPointsId,
                        principalTable: "AccruedPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccruedPointQuestionChoice_QuestionChoices_SelectedChoicesId",
                        column: x => x.SelectedChoicesId,
                        principalTable: "QuestionChoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccruedPointQuestionChoice_SelectedChoicesId",
                table: "AccruedPointQuestionChoice",
                column: "SelectedChoicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccruedPointQuestionChoice");

            migrationBuilder.AddColumn<Guid>(
                name: "AccruedPointId",
                table: "QuestionChoices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_AccruedPointId",
                table: "QuestionChoices",
                column: "AccruedPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionChoices_AccruedPoints_AccruedPointId",
                table: "QuestionChoices",
                column: "AccruedPointId",
                principalTable: "AccruedPoints",
                principalColumn: "Id");
        }
    }
}
