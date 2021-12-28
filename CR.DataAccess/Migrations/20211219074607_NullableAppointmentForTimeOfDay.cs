using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class NullableAppointmentForTimeOfDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId",
                principalTable: "TBL_Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                table: "TBL_TimeOfDays");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "TBL_TimeOfDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId",
                principalTable: "TBL_Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
