using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainsToDo.Migrations
{
    /// <inheritdoc />
    public partial class _202503311901 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Person_PersonId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Person_PersonId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Education_EducationId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Experience_ExperienceId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Person_PersonId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_User_UserId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Person_PersonId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_UserId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Project_EducationId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ExperienceId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Experience_PersonId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Education_PersonId",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Skills",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_PersonId",
                table: "Skills",
                newName: "IX_Skills_ResumeId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Project",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_PersonId",
                table: "Project",
                newName: "IX_Project_ResumeId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Education",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Resume",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "Resume",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Experience",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Education",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_userId",
                table: "Tasks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_ResumeId",
                table: "Experience",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_ResumeId",
                table: "Education",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Resume_ResumeId",
                table: "Project",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Resume_ResumeId",
                table: "Skills",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_User_userId",
                table: "Tasks",
                column: "userId",
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
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Resume_ResumeId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Resume_ResumeId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_User_userId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_userId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Experience_ResumeId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Education_ResumeId",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Skills",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_ResumeId",
                table: "Skills",
                newName: "IX_Skills_PersonId");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Project",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ResumeId",
                table: "Project",
                newName: "IX_Project_PersonId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Education",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "ResumeTemplateId",
                table: "Resume",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Resume",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Project",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceId",
                table: "Project",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Experience",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Education",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_UserId",
                table: "Resume",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_EducationId",
                table: "Project",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ExperienceId",
                table: "Project",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_PersonId",
                table: "Experience",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_PersonId",
                table: "Education",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Person_PersonId",
                table: "Education",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Person_PersonId",
                table: "Experience",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Education_EducationId",
                table: "Project",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Experience_ExperienceId",
                table: "Project",
                column: "ExperienceId",
                principalTable: "Experience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Person_PersonId",
                table: "Project",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_User_UserId",
                table: "Resume",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Person_PersonId",
                table: "Skills",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
