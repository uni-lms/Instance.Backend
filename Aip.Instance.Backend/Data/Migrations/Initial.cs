#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;


#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aip.Instance.Backend.Data.Migrations {
  /// <inheritdoc />
  public partial class Initial : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
        name: "Groups",
        columns: table => new {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Groups", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Internships",
        columns: table => new {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Internships", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Roles",
        columns: table => new {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "Sections",
        columns: table => new {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Sections", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "StaticFiles",
        columns: table => new {
          Id = table.Column<string>(type: "text", nullable: false),
          Checksum = table.Column<string>(type: "text", nullable: false),
          Filename = table.Column<string>(type: "text", nullable: false),
          Filepath = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_StaticFiles", x => x.Id); });

      migrationBuilder.CreateTable(
        name: "FlowInternship",
        columns: table => new {
          AssignedFlowsId = table.Column<Guid>(type: "uuid", nullable: false),
          InternshipsId = table.Column<Guid>(type: "uuid", nullable: false)
        },
        constraints: table => {
          table.PrimaryKey("PK_FlowInternship", x => new { x.AssignedFlowsId, x.InternshipsId });
          table.ForeignKey(
            name: "FK_FlowInternship_Groups_AssignedFlowsId",
            column: x => x.AssignedFlowsId,
            principalTable: "Groups",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_FlowInternship_Internships_InternshipsId",
            column: x => x.InternshipsId,
            principalTable: "Internships",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
          FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
          LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
          Patronymic = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
          PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
          PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
          RoleId = table.Column<Guid>(type: "uuid", nullable: false),
          FlowId = table.Column<Guid>(type: "uuid", nullable: true),
          InternshipId = table.Column<Guid>(type: "uuid", nullable: true)
        },
        constraints: table => {
          table.PrimaryKey("PK_Users", x => x.Id);
          table.ForeignKey(
            name: "FK_Users_Groups_FlowId",
            column: x => x.FlowId,
            principalTable: "Groups",
            principalColumn: "Id");
          table.ForeignKey(
            name: "FK_Users_Internships_InternshipId",
            column: x => x.InternshipId,
            principalTable: "Internships",
            principalColumn: "Id");
          table.ForeignKey(
            name: "FK_Users_Roles_RoleId",
            column: x => x.RoleId,
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        name: "FileContents",
        columns: table => new {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Title = table.Column<string>(type: "text", nullable: false),
          FileId = table.Column<string>(type: "text", nullable: true),
          InternshipId = table.Column<Guid>(type: "uuid", nullable: false),
          SectionId = table.Column<Guid>(type: "uuid", nullable: false),
          IsVisibleToStudents = table.Column<bool>(type: "boolean", nullable: false)
        },
        constraints: table => {
          table.PrimaryKey("PK_FileContents", x => x.Id);
          table.ForeignKey(
            name: "FK_FileContents_Internships_InternshipId",
            column: x => x.InternshipId,
            principalTable: "Internships",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_FileContents_Sections_SectionId",
            column: x => x.SectionId,
            principalTable: "Sections",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_FileContents_StaticFiles_FileId",
            column: x => x.FileId,
            principalTable: "StaticFiles",
            principalColumn: "Id");
        });

      migrationBuilder.InsertData(
        table: "Roles",
        columns: new[] { "Id", "Name" },
        values: new object[,] {
          { new Guid("9a6bcced-f1d3-4155-b691-480718ee78e4"), "PrimaryTutor" },
          { new Guid("c0aa6455-626c-49de-94fc-616b4a6b5605"), "Intern" },
          { new Guid("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"), "InvitedTutor" }
        });

      migrationBuilder.InsertData(
        table: "Sections",
        columns: new[] { "Id", "Name" },
        values: new object[,] {
          { new Guid("33fe1815-0e3b-4991-8add-c163a2286711"), "Зачёт" },
          { new Guid("965d07a0-8666-49a9-af0e-797944c65f5f"), "Лабораторные работы" },
          { new Guid("a19c6184-46c3-40fa-bef7-32ab966a4946"), "Лекции" },
          { new Guid("adb191be-1e90-46e2-9886-1bfe911a0d66"), "Самостоятельная работа студентов" },
          { new Guid("d10a8149-afb3-416b-8bfe-3dbbedbec517"), "Курсовой проект" },
          { new Guid("d2604c2a-550a-4f7e-8da8-1864e6337377"), "Экзамен" },
          { new Guid("fa013e01-8a58-4ba5-b426-72b790c3ba4f"), "Курсовая работа" }
        });

      migrationBuilder.CreateIndex(
        name: "IX_FileContents_FileId",
        table: "FileContents",
        column: "FileId");

      migrationBuilder.CreateIndex(
        name: "IX_FileContents_InternshipId",
        table: "FileContents",
        column: "InternshipId");

      migrationBuilder.CreateIndex(
        name: "IX_FileContents_SectionId",
        table: "FileContents",
        column: "SectionId");

      migrationBuilder.CreateIndex(
        name: "IX_FlowInternship_InternshipsId",
        table: "FlowInternship",
        column: "InternshipsId");

      migrationBuilder.CreateIndex(
        name: "IX_Groups_Name",
        table: "Groups",
        column: "Name",
        unique: true);

      migrationBuilder.CreateIndex(
        name: "IX_Users_Email",
        table: "Users",
        column: "Email");

      migrationBuilder.CreateIndex(
        name: "IX_Users_FlowId",
        table: "Users",
        column: "FlowId");

      migrationBuilder.CreateIndex(
        name: "IX_Users_InternshipId",
        table: "Users",
        column: "InternshipId");

      migrationBuilder.CreateIndex(
        name: "IX_Users_RoleId",
        table: "Users",
        column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
        name: "FileContents");

      migrationBuilder.DropTable(
        name: "FlowInternship");

      migrationBuilder.DropTable(
        name: "Users");

      migrationBuilder.DropTable(
        name: "Sections");

      migrationBuilder.DropTable(
        name: "StaticFiles");

      migrationBuilder.DropTable(
        name: "Groups");

      migrationBuilder.DropTable(
        name: "Internships");

      migrationBuilder.DropTable(
        name: "Roles");
    }
  }
}