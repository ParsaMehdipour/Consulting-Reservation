using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class addedSpecialtyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentSpecialtyId",
                table: "TBL_Specialties",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Specialties_ParentSpecialtyId",
                table: "TBL_Specialties",
                column: "ParentSpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Specialties_TBL_Specialties_ParentSpecialtyId",
                table: "TBL_Specialties",
                column: "ParentSpecialtyId",
                principalTable: "TBL_Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Specialties_TBL_Specialties_ParentSpecialtyId",
                table: "TBL_Specialties");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Specialties_ParentSpecialtyId",
                table: "TBL_Specialties");

            migrationBuilder.DropColumn(
                name: "ParentSpecialtyId",
                table: "TBL_Specialties");
        }
    }
}
