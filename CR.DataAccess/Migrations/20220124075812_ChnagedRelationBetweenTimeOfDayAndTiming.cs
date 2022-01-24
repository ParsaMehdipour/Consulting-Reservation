using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChnagedRelationBetweenTimeOfDayAndTiming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Timings_TimingId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropIndex(
                name: "IX_TBL_TimeOfDays_TimingId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "TimingId",
                table: "TBL_TimeOfDays");

            migrationBuilder.AddColumn<string>(
                name: "FinishHour",
                table: "TBL_TimeOfDays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartHour",
                table: "TBL_TimeOfDays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimingType",
                table: "TBL_TimeOfDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishHour",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "TimingType",
                table: "TBL_TimeOfDays");

            migrationBuilder.AddColumn<long>(
                name: "TimingId",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
    }
}
