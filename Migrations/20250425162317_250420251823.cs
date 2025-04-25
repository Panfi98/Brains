using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _250420251823 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "InfoSkill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ResumeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoSkill_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoSkill_ResumeId",
                table: "InfoSkill",
                column: "ResumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoSkill");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Skills",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
