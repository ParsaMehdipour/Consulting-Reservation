using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class MadeSpecialityNuulable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                table: "TBL_ExpertInformations");

            migrationBuilder.AlterColumn<long>(
                name: "SpecialtyId",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                table: "TBL_ExpertInformations",
                column: "SpecialtyId",
                principalTable: "TBL_Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                table: "TBL_ExpertInformations");

            migrationBuilder.AlterColumn<long>(
                name: "SpecialtyId",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                table: "TBL_ExpertInformations",
                column: "SpecialtyId",
                principalTable: "TBL_Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
