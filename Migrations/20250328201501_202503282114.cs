using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _202503282114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "names",
                table: "Tasks",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tasks",
                newName: "names");
        }
    }
}
