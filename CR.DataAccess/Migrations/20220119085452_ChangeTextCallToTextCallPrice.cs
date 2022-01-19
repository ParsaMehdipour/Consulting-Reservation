using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangeTextCallToTextCallPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextCall",
                table: "TBL_TimeOfDays",
                newName: "TextCallPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextCallPrice",
                table: "TBL_TimeOfDays",
                newName: "TextCall");
        }
    }
}
