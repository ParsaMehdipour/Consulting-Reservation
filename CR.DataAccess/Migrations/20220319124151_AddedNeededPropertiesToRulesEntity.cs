using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedNeededPropertiesToRulesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentContent",
                table: "TBL_Rule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherContent",
                table: "TBL_Rule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentContent",
                table: "TBL_Rule",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentContent",
                table: "TBL_Rule");

            migrationBuilder.DropColumn(
                name: "OtherContent",
                table: "TBL_Rule");

            migrationBuilder.DropColumn(
                name: "PaymentContent",
                table: "TBL_Rule");
        }
    }
}
