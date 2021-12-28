using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedBirthDate_StringPropToInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthDate_String",
                table: "TBL_ExpertInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthDate_String",
                table: "TBL_ConsumersInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate_String",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "BirthDate_String",
                table: "TBL_ConsumersInformations");
        }
    }
}
