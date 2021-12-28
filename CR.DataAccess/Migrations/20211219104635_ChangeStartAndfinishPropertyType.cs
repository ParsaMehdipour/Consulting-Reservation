using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangeStartAndfinishPropertyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finish",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "TBL_TimeOfDays");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "TBL_TimeOfDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TBL_TimeOfDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TBL_TimeOfDays");

            migrationBuilder.AddColumn<int>(
                name: "Finish",
                table: "TBL_TimeOfDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Start",
                table: "TBL_TimeOfDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
