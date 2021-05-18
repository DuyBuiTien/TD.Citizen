using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TD.CongDan.Infrastructure.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: " JobApplieds",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecruitmentId = table.Column<int>(type: "int", nullable: false),
                    CVFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ JobApplieds", x => new { x.UserName, x.RecruitmentId });
                    table.ForeignKey(
                        name: "FK_ JobApplieds_Recruitments_RecruitmentId",
                        column: x => x.RecruitmentId,
                        principalTable: "Recruitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSaveds",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecruitmentId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSaveds", x => new { x.UserName, x.RecruitmentId });
                    table.ForeignKey(
                        name: "FK_JobSaveds_Recruitments_RecruitmentId",
                        column: x => x.RecruitmentId,
                        principalTable: "Recruitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ JobApplieds_RecruitmentId",
                table: " JobApplieds",
                column: "RecruitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSaveds_RecruitmentId",
                table: "JobSaveds",
                column: "RecruitmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: " JobApplieds");

            migrationBuilder.DropTable(
                name: "JobSaveds");
        }
    }
}
