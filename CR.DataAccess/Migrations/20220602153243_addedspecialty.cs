using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class addedspecialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "TBL_Specialties",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Specialties_TBL_Specialties_SpecialtyId",
                table: "TBL_Specialties");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Specialties_SpecialtyId",
                table: "TBL_Specialties");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "TBL_Specialties");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "TBL_Specialties");
        }
    }
}
