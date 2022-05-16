using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddSiteSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SiteBanner",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteBannerColor",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteFooterLogo",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopBoxImage1",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopBoxImage2",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopBoxImage3",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopBoxText1",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopBoxText2",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopBoxText3",
                table: "TBL_Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteBanner",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "SiteBannerColor",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "SiteFooterLogo",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "TopBoxImage1",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "TopBoxImage2",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "TopBoxImage3",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "TopBoxText1",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "TopBoxText2",
                table: "TBL_Settings");

            migrationBuilder.DropColumn(
                name: "TopBoxText3",
                table: "TBL_Settings");
        }
    }
}
