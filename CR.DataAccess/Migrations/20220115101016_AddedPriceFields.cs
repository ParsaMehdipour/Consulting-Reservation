using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedPriceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "TBL_ConsumersInformations");

            migrationBuilder.RenameColumn(
                name: "IsFreeOfCharge",
                table: "TBL_ExpertInformations",
                newName: "UseVoiceCall");

            migrationBuilder.AddColumn<long>(
                name: "PhoneCallPrice",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TextCall",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VoiceCallPrice",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PhoneCallPrice",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TextCallPrice",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "UsePhoneCall",
                table: "TBL_ExpertInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseTextCall",
                table: "TBL_ExpertInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "VoiceCallPrice",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneCallPrice",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "TextCall",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "VoiceCallPrice",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "PhoneCallPrice",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "TextCallPrice",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "UsePhoneCall",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "UseTextCall",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "VoiceCallPrice",
                table: "TBL_ExpertInformations");

            migrationBuilder.RenameColumn(
                name: "UseVoiceCall",
                table: "TBL_ExpertInformations",
                newName: "IsFreeOfCharge");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "TBL_ConsumersInformations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
