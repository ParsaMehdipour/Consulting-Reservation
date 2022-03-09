using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedAudioToChatUserMseeages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Audio",
                table: "TBL_ChatUserMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audio",
                table: "TBL_ChatUserMessages");
        }
    }
}
