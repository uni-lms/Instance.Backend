using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSolutionChecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckedAt",
                table: "SolutionChecks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SolutionChecks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedAt",
                table: "SolutionChecks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SolutionChecks");
        }
    }
}
