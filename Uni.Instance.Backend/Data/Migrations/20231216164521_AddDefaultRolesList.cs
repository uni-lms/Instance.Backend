using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Uni.Instance.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRolesList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9a6bcced-f1d3-4155-b691-480718ee78e4"), "Admin" },
                    { new Guid("c0aa6455-626c-49de-94fc-616b4a6b5605"), "Student" },
                    { new Guid("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"), "Tutor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9a6bcced-f1d3-4155-b691-480718ee78e4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0aa6455-626c-49de-94fc-616b4a6b5605"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"));
        }
    }
}
