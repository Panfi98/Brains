using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _190420252218 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:Status.status", "not_started,in_progress,finished")
                .OldAnnotation("Npgsql:Enum:status", "not_started,in_progress,finished");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reference",
                type: "Status",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:status", "not_started,in_progress,finished")
                .OldAnnotation("Npgsql:Enum:Status.status", "not_started,in_progress,finished");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reference",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "Status");
        }
    }
}
