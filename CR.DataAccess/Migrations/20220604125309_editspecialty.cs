using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class editspecialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Specialties_TBL_Specialties_SpecialtyId",
                table: "TBL_Specialties");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Specialties_SpecialtyId",
                table: "TBL_Specialties");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "TBL_Specialties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SpecialtyId",
                table: "TBL_Specialties",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Specialties_SpecialtyId",
                table: "TBL_Specialties",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Specialties_TBL_Specialties_SpecialtyId",
                table: "TBL_Specialties",
                column: "SpecialtyId",
                principalTable: "TBL_Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
