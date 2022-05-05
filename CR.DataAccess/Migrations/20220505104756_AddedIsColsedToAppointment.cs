using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedIsColsedToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "TBL_Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "TBL_Appointments");
        }
    }
}
