using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedTwoIdsForFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsumerInformationId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExpertInformationId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Factors_ConsumerInformationId",
                table: "TBL_Factors",
                column: "ConsumerInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Factors_ExpertInformationId",
                table: "TBL_Factors",
                column: "ExpertInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Factors_TBL_ConsumersInformations_ConsumerInformationId",
                table: "TBL_Factors",
                column: "ConsumerInformationId",
                principalTable: "TBL_ConsumersInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Factors_TBL_ExpertInformations_ExpertInformationId",
                table: "TBL_Factors",
                column: "ExpertInformationId",
                principalTable: "TBL_ExpertInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Factors_TBL_ConsumersInformations_ConsumerInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Factors_TBL_ExpertInformations_ExpertInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Factors_ConsumerInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Factors_ExpertInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropColumn(
                name: "ConsumerInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropColumn(
                name: "ExpertInformationId",
                table: "TBL_Factors");
        }
    }
}
