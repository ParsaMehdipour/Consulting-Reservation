using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedNeededFiledsToTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardHolderPAN",
                table: "TBL_FinancialTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "TBL_FinancialTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SaleReferenceId",
                table: "TBL_FinancialTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TransactionNumber",
                table: "TBL_FinancialTransactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolderPAN",
                table: "TBL_FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "RefId",
                table: "TBL_FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "SaleReferenceId",
                table: "TBL_FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "TBL_FinancialTransactions");
        }
    }
}
