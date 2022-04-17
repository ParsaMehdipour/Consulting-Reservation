using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangedRelationBetweenAppointmentAndTimeOfDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Appointments_TimeOfDayId",
                table: "TBL_Appointments",
                column: "TimeOfDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Appointments_TBL_TimeOfDays_TimeOfDayId",
                table: "TBL_Appointments",
                column: "TimeOfDayId",
                principalTable: "TBL_TimeOfDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Appointments_TBL_TimeOfDays_TimeOfDayId",
                table: "TBL_Appointments");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Appointments_TimeOfDayId",
                table: "TBL_Appointments");

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId",
                principalTable: "TBL_Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
