using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedCommissionAndDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CommissionAndDiscountId",
                table: "TBL_ExpertInformations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBL_CommissionAndDiscounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneCallCommission = table.Column<double>(type: "float", nullable: false),
                    VoiceCallCommission = table.Column<double>(type: "float", nullable: false),
                    TextCallCommission = table.Column<double>(type: "float", nullable: false),
                    PhoneCallDiscount = table.Column<double>(type: "float", nullable: false),
                    VoiceCallDiscount = table.Column<double>(type: "float", nullable: false),
                    TextCallDiscount = table.Column<double>(type: "float", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CommissionAndDiscounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertInformations_CommissionAndDiscountId",
                table: "TBL_ExpertInformations",
                column: "CommissionAndDiscountId",
                unique: true,
                filter: "[CommissionAndDiscountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_CommissionAndDiscounts_CommissionAndDiscountId",
                table: "TBL_ExpertInformations",
                column: "CommissionAndDiscountId",
                principalTable: "TBL_CommissionAndDiscounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ExpertInformations_TBL_CommissionAndDiscounts_CommissionAndDiscountId",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropTable(
                name: "TBL_CommissionAndDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_TBL_ExpertInformations_CommissionAndDiscountId",
                table: "TBL_ExpertInformations");

            migrationBuilder.DropColumn(
                name: "CommissionAndDiscountId",
                table: "TBL_ExpertInformations");
        }
    }
}
