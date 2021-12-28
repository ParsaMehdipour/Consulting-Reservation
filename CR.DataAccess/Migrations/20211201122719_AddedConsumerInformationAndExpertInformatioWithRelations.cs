using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class AddedConsumerInformationAndExpertInformatioWithRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsumerInformationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ExpertInformationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TBL_ConsumersInformations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ConsumersInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertInformations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Specialties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumerInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Appointments_TBL_ConsumersInformations_ConsumerInformationId",
                        column: x => x.ConsumerInformationId,
                        principalTable: "TBL_ConsumersInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Appointments_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Days",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Days_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertSpecialties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtyId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertSpecialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertSpecialties_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertSpecialties_TBL_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "TBL_Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TimeOfDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<int>(type: "int", nullable: false),
                    Finish = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<long>(type: "bigint", nullable: false),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TimeOfDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_TimeOfDays_TBL_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "TBL_Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_TimeOfDays_TBL_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "TBL_Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TBL_TimeOfDays_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConsumerInformationId",
                table: "AspNetUsers",
                column: "ConsumerInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExpertInformationId",
                table: "AspNetUsers",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Appointments_ConsumerInformationId",
                table: "TBL_Appointments",
                column: "ConsumerInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Appointments_ExpertInformationId",
                table: "TBL_Appointments",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Days_ExpertInformationId",
                table: "TBL_Days",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertSpecialties_ExpertInformationId",
                table: "TBL_ExpertSpecialties",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertSpecialties_SpecialtyId",
                table: "TBL_ExpertSpecialties",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_DayId",
                table: "TBL_TimeOfDays",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_ExpertInformationId",
                table: "TBL_TimeOfDays",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBL_ConsumersInformations_ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBL_ExpertInformations_ExpertInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TBL_ExpertSpecialties");

            migrationBuilder.DropTable(
                name: "TBL_TimeOfDays");

            migrationBuilder.DropTable(
                name: "TBL_Specialties");

            migrationBuilder.DropTable(
                name: "TBL_Appointments");

            migrationBuilder.DropTable(
                name: "TBL_Days");

            migrationBuilder.DropTable(
                name: "TBL_ConsumersInformations");

            migrationBuilder.DropTable(
                name: "TBL_ExpertInformations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ExpertInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConsumerInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpertInformationId",
                table: "AspNetUsers");
        }
    }
}
