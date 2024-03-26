using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aip.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameVisibilityField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisibleToStudents",
                table: "TextContents",
                newName: "IsVisibleToInterns");

            migrationBuilder.RenameColumn(
                name: "IsVisibleToStudents",
                table: "LinkContents",
                newName: "IsVisibleToInterns");

            migrationBuilder.RenameColumn(
                name: "IsVisibleToStudents",
                table: "FileContents",
                newName: "IsVisibleToInterns");

            migrationBuilder.RenameColumn(
                name: "IsVisibleToStudents",
                table: "Assignments",
                newName: "IsVisibleToInterns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisibleToInterns",
                table: "TextContents",
                newName: "IsVisibleToStudents");

            migrationBuilder.RenameColumn(
                name: "IsVisibleToInterns",
                table: "LinkContents",
                newName: "IsVisibleToStudents");

            migrationBuilder.RenameColumn(
                name: "IsVisibleToInterns",
                table: "FileContents",
                newName: "IsVisibleToStudents");

            migrationBuilder.RenameColumn(
                name: "IsVisibleToInterns",
                table: "Assignments",
                newName: "IsVisibleToStudents");
        }
    }
}
