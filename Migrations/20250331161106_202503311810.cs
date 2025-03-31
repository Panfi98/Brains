using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _202503311810 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverLetter_User_JobApplicationalId",
                table: "CoverLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_User_ResumeId",
                table: "JobApplication");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverLetter_JobApplication_JobApplicationalId",
                table: "CoverLetter",
                column: "JobApplicationalId",
                principalTable: "JobApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Resume_ResumeId",
                table: "JobApplication",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverLetter_JobApplication_JobApplicationalId",
                table: "CoverLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Resume_ResumeId",
                table: "JobApplication");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverLetter_User_JobApplicationalId",
                table: "CoverLetter",
                column: "JobApplicationalId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_User_ResumeId",
                table: "JobApplication",
                column: "ResumeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
