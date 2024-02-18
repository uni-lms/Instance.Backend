#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;


namespace Aip.Instance.Backend.Data.Migrations {
  /// <inheritdoc />
  public partial class RemakeRolesSystem : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AddColumn<Guid>(
        name: "PrimaryTutorId",
        table: "Internships",
        type: "uuid",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

      migrationBuilder.CreateIndex(
        name: "IX_Internships_PrimaryTutorId",
        table: "Internships",
        column: "PrimaryTutorId");

      migrationBuilder.AddForeignKey(
        name: "FK_Internships_Users_PrimaryTutorId",
        table: "Internships",
        column: "PrimaryTutorId",
        principalTable: "Users",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropForeignKey(
        name: "FK_Internships_Users_PrimaryTutorId",
        table: "Internships");

      migrationBuilder.DropIndex(
        name: "IX_Internships_PrimaryTutorId",
        table: "Internships");

      migrationBuilder.DropColumn(
        name: "PrimaryTutorId",
        table: "Internships");
    }
  }
}