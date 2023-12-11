using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSequenceNumberToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "MultipleChoiceQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "MultipleChoiceQuestions");
        }
    }
}
