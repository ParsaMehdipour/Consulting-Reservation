using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class RemoveSaleRefId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleReferenceId",
                table: "TBL_Factors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SaleReferenceId",
                table: "TBL_Factors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
