using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceAvailiabilityByDateWithFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSince",
                table: "FileContents");

            migrationBuilder.DropColumn(
                name: "AvailableUntil",
                table: "FileContents");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleToStudents",
                table: "FileContents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisibleToStudents",
                table: "FileContents");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableSince",
                table: "FileContents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableUntil",
                table: "FileContents",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
