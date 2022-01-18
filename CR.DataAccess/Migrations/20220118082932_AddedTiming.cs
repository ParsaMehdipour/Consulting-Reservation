using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedTiming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TBL_TimeOfDays");

            migrationBuilder.AddColumn<long>(
                name: "TimingId",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TBL_Timings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimingType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Timings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_TimingId",
                table: "TBL_TimeOfDays",
                column: "TimingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Timings_TimingId",
                table: "TBL_TimeOfDays",
                column: "TimingId",
                principalTable: "TBL_Timings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Timings_TimingId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropTable(
                name: "TBL_Timings");

            migrationBuilder.DropIndex(
                name: "IX_TBL_TimeOfDays_TimingId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "TimingId",
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
    }
}
