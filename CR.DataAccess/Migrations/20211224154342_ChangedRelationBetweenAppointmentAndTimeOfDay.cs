using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangedRelationBetweenAppointmentAndTimeOfDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.AddColumn<long>(
                name: "TimeOfDayId",
                table: "TBL_Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "TimeOfDayId",
                table: "TBL_Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId");
        }
    }
}
