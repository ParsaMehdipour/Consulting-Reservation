using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangedPropetyNamesInExpertExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "TBL_ExpertExperiences");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TBL_ExpertExperiences");

            migrationBuilder.RenameColumn(
                name: "StartDate_String",
                table: "TBL_ExpertExperiences",
                newName: "StartYear");

            migrationBuilder.RenameColumn(
                name: "HospitalName",
                table: "TBL_ExpertExperiences",
                newName: "FinishYear");

            migrationBuilder.RenameColumn(
                name: "FinishDate_String",
                table: "TBL_ExpertExperiences",
                newName: "ClinicName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartYear",
                table: "TBL_ExpertExperiences",
                newName: "StartDate_String");

            migrationBuilder.RenameColumn(
                name: "FinishYear",
                table: "TBL_ExpertExperiences",
                newName: "HospitalName");

            migrationBuilder.RenameColumn(
                name: "ClinicName",
                table: "TBL_ExpertExperiences",
                newName: "FinishDate_String");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "TBL_ExpertExperiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TBL_ExpertExperiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
