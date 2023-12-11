using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSavingSelectedChoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
