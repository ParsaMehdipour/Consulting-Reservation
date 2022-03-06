using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class ChatUserAndChatUserMessgesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_ChatUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ChatUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ChatUsers_AspNetUsers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ChatUsers_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ChatUserMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageFlag = table.Column<int>(type: "int", nullable: false),
                    ChatUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ChatUserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ChatUserMessages_TBL_ChatUsers_ChatUserId",
                        column: x => x.ChatUserId,
                        principalTable: "TBL_ChatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ChatUserMessages_ChatUserId",
                table: "TBL_ChatUserMessages",
                column: "ChatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ChatUsers_ConsumerId",
                table: "TBL_ChatUsers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ChatUsers_ExpertInformationId",
                table: "TBL_ChatUsers",
                column: "ExpertInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ChatUserMessages");

            migrationBuilder.DropTable(
                name: "TBL_ChatUsers");
        }
    }
}
