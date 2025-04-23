using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _230420251903 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Person_PersonId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_Person_PersonId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_PersonId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Resume");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Education",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_PersonId",
                table: "Education",
                newName: "IX_Education_ResumeId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Resume",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_UserId",
                table: "Resume",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_User_UserId",
                table: "Resume",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_User_UserId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_UserId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Resume");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Education",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ResumeId",
                table: "Education",
                newName: "IX_Education_PersonId");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Resume",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_PersonId",
                table: "Resume",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Person_PersonId",
                table: "Education",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_Person_PersonId",
                table: "Resume",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
