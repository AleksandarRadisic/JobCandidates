using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobCandidates.Persistence.Migrations
{
    public partial class AddedCandidatesToSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobCandidates_JobCandidateId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobCandidateId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobCandidateId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "JobCandidateSkill",
                columns: table => new
                {
                    JobCandidatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateSkill", x => new { x.JobCandidatesId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_JobCandidateSkill_JobCandidates_JobCandidatesId",
                        column: x => x.JobCandidatesId,
                        principalTable: "JobCandidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobCandidateSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateSkill_SkillsId",
                table: "JobCandidateSkill",
                column: "SkillsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCandidateSkill");

            migrationBuilder.AddColumn<Guid>(
                name: "JobCandidateId",
                table: "Skills",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobCandidateId",
                table: "Skills",
                column: "JobCandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobCandidates_JobCandidateId",
                table: "Skills",
                column: "JobCandidateId",
                principalTable: "JobCandidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
