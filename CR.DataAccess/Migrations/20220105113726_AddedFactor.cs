using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FactorId",
                table: "TBL_Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBL_Factors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactorNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FactorStatus = table.Column<int>(type: "int", nullable: false),
                    CardHolderPAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleReferenceId = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Factors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Appointments_FactorId",
                table: "TBL_Appointments",
                column: "FactorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Appointments_TBL_Factors_FactorId",
                table: "TBL_Appointments",
                column: "FactorId",
                principalTable: "TBL_Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Appointments_TBL_Factors_FactorId",
                table: "TBL_Appointments");

            migrationBuilder.DropTable(
                name: "TBL_Factors");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Appointments_FactorId",
                table: "TBL_Appointments");

            migrationBuilder.DropColumn(
                name: "FactorId",
                table: "TBL_Appointments");
        }
    }
}
