using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedPriceFieldsToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CommissionPrice",
                table: "TBL_Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DiscountPrice",
                table: "TBL_Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RawPrice",
                table: "TBL_Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionPrice",
                table: "TBL_Appointments");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "TBL_Appointments");

            migrationBuilder.DropColumn(
                name: "RawPrice",
                table: "TBL_Appointments");
        }
    }
}
