using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChangeInformationIdsToNullForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBL_ConsumersInformations_ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBL_ExpertInformations_ExpertInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ExpertInformationId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "ExpertId",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ConsumerId",
                table: "TBL_ConsumersInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ExpertInformationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerInformationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConsumerInformationId",
                table: "AspNetUsers",
                column: "ConsumerInformationId",
                unique: true,
                filter: "[ConsumerInformationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExpertInformationId",
                table: "AspNetUsers",
                column: "ExpertInformationId",
                unique: true,
                filter: "[ExpertInformationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TBL_ConsumersInformations_ConsumerInformationId",
                table: "AspNetUsers",
                column: "ConsumerInformationId",
                principalTable: "TBL_ConsumersInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TBL_ExpertInformations_ExpertInformationId",
                table: "AspNetUsers",
                column: "ExpertInformationId",
                principalTable: "TBL_ExpertInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBL_ConsumersInformations_ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBL_ExpertInformations_ExpertInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ExpertInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                table: "TBL_ConsumersInformations");

            migrationBuilder.AlterColumn<long>(
                name: "ExpertInformationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerInformationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConsumerInformationId",
                table: "AspNetUsers",
                column: "ConsumerInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExpertInformationId",
                table: "AspNetUsers",
                column: "ExpertInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TBL_ConsumersInformations_ConsumerInformationId",
                table: "AspNetUsers",
                column: "ConsumerInformationId",
                principalTable: "TBL_ConsumersInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TBL_ExpertInformations_ExpertInformationId",
                table: "AspNetUsers",
                column: "ExpertInformationId",
                principalTable: "TBL_ExpertInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
