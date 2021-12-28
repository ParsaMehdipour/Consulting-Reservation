using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedExtraTablesForExpertInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicAddress",
                table: "TBL_ExpertInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicName",
                table: "TBL_ExpertInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBL_ExpertExperiences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertExperiences_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertImages_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertMemberships",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertMemberships_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertPrizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrizeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertPrizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertPrizes_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertStudies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeOfEducation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertStudies_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertSubscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertSubscriptions_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertExperiences_ExpertInformationId",
                table: "TBL_ExpertExperiences",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertImages_ExpertInformationId",
                table: "TBL_ExpertImages",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertMemberships_ExpertInformationId",
                table: "TBL_ExpertMemberships",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertPrizes_ExpertInformationId",
                table: "TBL_ExpertPrizes",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertStudies_ExpertInformationId",
                table: "TBL_ExpertStudies",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertSubscriptions_ExpertInformationId",
                table: "TBL_ExpertSubscriptions",
                column: "ExpertInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ExpertExperiences");

            migrationBuilder.DropTable(
                name: "TBL_ExpertImages");

            migrationBuilder.DropTable(
                name: "TBL_ExpertMemberships");

            migrationBuilder.DropTable(
                name: "TBL_ExpertPrizes");

            migrationBuilder.DropTable(
                name: "TBL_ExpertStudies");

            migrationBuilder.DropTable(
                name: "TBL_ExpertSubscriptions");

            migrationBuilder.DropColumn(
                name: "ClinicAddress",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "ClinicName",
                table: "TBL_ExpertInformations");
        }
    }
}
