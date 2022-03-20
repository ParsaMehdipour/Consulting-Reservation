using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedMessageTypeToChatUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "TBL_ChatUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "TBL_ChatUsers");
        }
    }
}
