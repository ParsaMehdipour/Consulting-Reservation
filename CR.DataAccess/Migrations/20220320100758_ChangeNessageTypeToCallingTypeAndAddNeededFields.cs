using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangeNessageTypeToCallingTypeAndAddNeededFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "TBL_ChatUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AppointmentDate_String",
                table: "TBL_ChatUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "TBL_ChatUsers");

            migrationBuilder.DropColumn(
                name: "AppointmentDate_String",
                table: "TBL_ChatUsers");
        }
    }
}
