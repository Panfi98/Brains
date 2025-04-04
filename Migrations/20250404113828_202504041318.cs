using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _202504041318 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Education",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ResumeId",
                table: "Education",
                newName: "IX_Education_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Person_PersonId",
                table: "Education",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Person_PersonId",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Education",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_PersonId",
                table: "Education",
                newName: "IX_Education_ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
