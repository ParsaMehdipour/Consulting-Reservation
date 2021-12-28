using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddeddExtraPropertiesToExpertInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFreeOfCharge",
                table: "TBL_ExpertInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "TBL_ExpertInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TBL_ExpertInformations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFreeOfCharge",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TBL_ExpertInformations");
        }
    }
}
