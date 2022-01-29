using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class MadeTwoIdsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Factors_TBL_ConsumersInformations_ConsumerInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Factors_TBL_ExpertInformations_ExpertInformationId",
                table: "TBL_Factors");

            migrationBuilder.AlterColumn<long>(
                name: "ExpertInformationId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerInformationId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Factors_TBL_ConsumersInformations_ConsumerInformationId",
                table: "TBL_Factors",
                column: "ConsumerInformationId",
                principalTable: "TBL_ConsumersInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Factors_TBL_ExpertInformations_ExpertInformationId",
                table: "TBL_Factors",
                column: "ExpertInformationId",
                principalTable: "TBL_ExpertInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Factors_TBL_ConsumersInformations_ConsumerInformationId",
                table: "TBL_Factors");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Factors_TBL_ExpertInformations_ExpertInformationId",
                table: "TBL_Factors");

            migrationBuilder.AlterColumn<long>(
                name: "ExpertInformationId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerInformationId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
