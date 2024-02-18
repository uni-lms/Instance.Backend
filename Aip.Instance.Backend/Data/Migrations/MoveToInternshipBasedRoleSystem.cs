#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;


namespace Aip.Instance.Backend.Data.Migrations {
  /// <inheritdoc />
  public partial class MoveToInternshipBasedRoleSystem : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropForeignKey(
        name: "FK_Internships_Users_PrimaryTutorId",
        table: "Internships");

      migrationBuilder.DropForeignKey(
        name: "FK_Users_Internships_InternshipId",
        table: "Users");

      migrationBuilder.DropForeignKey(
        name: "FK_Users_Roles_RoleId",
        table: "Users");

      migrationBuilder.DropIndex(
        name: "IX_Users_InternshipId",
        table: "Users");

      migrationBuilder.DropIndex(
        name: "IX_Users_RoleId",
        table: "Users");

      migrationBuilder.DropIndex(
        name: "IX_Internships_PrimaryTutorId",
        table: "Internships");

      migrationBuilder.DropColumn(
        name: "InternshipId",
        table: "Users");

      migrationBuilder.DropColumn(
        name: "RoleId",
        table: "Users");

      migrationBuilder.DropColumn(
        name: "PrimaryTutorId",
        table: "Internships");

      migrationBuilder.CreateTable(
        name: "InternshipBasedRoles",
        columns: table => new {
          InternshipId = table.Column<Guid>(type: "uuid", nullable: false),
          UserId = table.Column<Guid>(type: "uuid", nullable: false),
          RoleId = table.Column<Guid>(type: "uuid", nullable: false)
        },
        constraints: table => {
          table.PrimaryKey("PK_InternshipBasedRoles", x => new { x.UserId, x.InternshipId });
          table.ForeignKey(
            name: "FK_InternshipBasedRoles_Internships_InternshipId",
            column: x => x.InternshipId,
            principalTable: "Internships",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_InternshipBasedRoles_Roles_RoleId",
            column: x => x.RoleId,
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_InternshipBasedRoles_Users_UserId",
            column: x => x.UserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_InternshipBasedRoles_InternshipId",
        table: "InternshipBasedRoles",
        column: "InternshipId");

      migrationBuilder.CreateIndex(
        name: "IX_InternshipBasedRoles_RoleId",
        table: "InternshipBasedRoles",
        column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
        name: "InternshipBasedRoles");

      migrationBuilder.AddColumn<Guid>(
        name: "InternshipId",
        table: "Users",
        type: "uuid",
        nullable: true);

      migrationBuilder.AddColumn<Guid>(
        name: "RoleId",
        table: "Users",
        type: "uuid",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

      migrationBuilder.AddColumn<Guid>(
        name: "PrimaryTutorId",
        table: "Internships",
        type: "uuid",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

      migrationBuilder.CreateIndex(
        name: "IX_Users_InternshipId",
        table: "Users",
        column: "InternshipId");

      migrationBuilder.CreateIndex(
        name: "IX_Users_RoleId",
        table: "Users",
        column: "RoleId");

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

      migrationBuilder.AddForeignKey(
        name: "FK_Users_Internships_InternshipId",
        table: "Users",
        column: "InternshipId",
        principalTable: "Internships",
        principalColumn: "Id");

      migrationBuilder.AddForeignKey(
        name: "FK_Users_Roles_RoleId",
        table: "Users",
        column: "RoleId",
        principalTable: "Roles",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
    }
  }
}