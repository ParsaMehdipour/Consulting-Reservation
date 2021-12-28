using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class RemovedExpertSpecialitiesTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ExpertSpecialties");

            migrationBuilder.AddColumn<long>(
                name: "SpecialtyId",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertInformations_SpecialtyId",
                table: "TBL_ExpertInformations",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                table: "TBL_ExpertInformations",
                column: "SpecialtyId",
                principalTable: "TBL_Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropIndex(
                name: "IX_TBL_ExpertInformations_SpecialtyId",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "TBL_ExpertInformations");

            migrationBuilder.CreateTable(
                name: "TBL_ExpertSpecialties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    SpecialtyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertSpecialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertSpecialties_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertSpecialties_TBL_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "TBL_Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertSpecialties_ExpertInformationId",
                table: "TBL_ExpertSpecialties",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertSpecialties_SpecialtyId",
                table: "TBL_ExpertSpecialties",
                column: "SpecialtyId");
        }
    }
}
