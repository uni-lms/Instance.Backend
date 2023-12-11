using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TimeLimit = table.Column<TimeSpan>(type: "interval", nullable: true),
                    IsQuestionsShuffled = table.Column<bool>(type: "boolean", nullable: false),
                    AvailableUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    MaximumPoints = table.Column<int>(type: "integer", nullable: false),
                    IsMultipleChoicesAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    IsGivingPointsForIncompleteAnswersEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    QuizContentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceQuestions_QuizContents_QuizContentId",
                        column: x => x.QuizContentId,
                        principalTable: "QuizContents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizPassAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizPassAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizPassAttempts_QuizContents_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizPassAttempts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    AmountOfPoints = table.Column<int>(type: "integer", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    MultipleChoiceQuestionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionChoices_MultipleChoiceQuestions_MultipleChoiceQuest~",
                        column: x => x.MultipleChoiceQuestionId,
                        principalTable: "MultipleChoiceQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccruedPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountOfPoints = table.Column<int>(type: "integer", nullable: false),
                    QuizPassAttemptId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccruedPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccruedPoints_MultipleChoiceQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "MultipleChoiceQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccruedPoints_QuizPassAttempts_QuizPassAttemptId",
                        column: x => x.QuizPassAttemptId,
                        principalTable: "QuizPassAttempts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccruedPoints_QuestionId",
                table: "AccruedPoints",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccruedPoints_QuizPassAttemptId",
                table: "AccruedPoints",
                column: "QuizPassAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceQuestions_QuizContentId",
                table: "MultipleChoiceQuestions",
                column: "QuizContentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_MultipleChoiceQuestionId",
                table: "QuestionChoices",
                column: "MultipleChoiceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizPassAttempts_QuizId",
                table: "QuizPassAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizPassAttempts_UserId",
                table: "QuizPassAttempts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccruedPoints");

            migrationBuilder.DropTable(
                name: "QuestionChoices");

            migrationBuilder.DropTable(
                name: "QuizPassAttempts");

            migrationBuilder.DropTable(
                name: "MultipleChoiceQuestions");

            migrationBuilder.DropTable(
                name: "QuizContents");
        }
    }
}
