using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedRelationBetweenAppointmentAndChatUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "TBL_ChatUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ChatUsers_AppointmentId",
                table: "TBL_ChatUsers",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ChatUsers_TBL_Appointments_AppointmentId",
                table: "TBL_ChatUsers",
                column: "AppointmentId",
                principalTable: "TBL_Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ChatUsers_TBL_Appointments_AppointmentId",
                table: "TBL_ChatUsers");

            migrationBuilder.DropIndex(
                name: "IX_TBL_ChatUsers_AppointmentId",
                table: "TBL_ChatUsers");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "TBL_ChatUsers");
        }
    }
}
