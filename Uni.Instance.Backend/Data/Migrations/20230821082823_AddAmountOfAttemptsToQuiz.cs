using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountOfAttemptsToQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfAllowedAttempts",
                table: "QuizContents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfAllowedAttempts",
                table: "QuizContents");
        }
    }
}
