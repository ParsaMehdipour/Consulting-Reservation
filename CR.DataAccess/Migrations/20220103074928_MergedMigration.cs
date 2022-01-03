using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CR.DataAccess.Migrations
{
    public partial class MergedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ConsumersInformations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ConsumersInformations", x => x.Id);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    BirthDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFreeOfCharge = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertId = table.Column<long>(type: "bigint", nullable: false),
                    SpecialtyId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertInformations_TBL_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "TBL_Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserFlag = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConsumerInformationId = table.Column<long>(type: "bigint", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TBL_ConsumersInformations_ConsumerInformationId",
                        column: x => x.ConsumerInformationId,
                        principalTable: "TBL_ConsumersInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentStatus = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumerInformationId = table.Column<long>(type: "bigint", nullable: false),
                    TimeOfDayId = table.Column<long>(type: "bigint", nullable: false),
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
                    Date_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "TBL_ExpertExperiences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertExperiences_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertImages_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertMemberships",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertMemberships_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertPrizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrizeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertPrizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertPrizes_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertStudies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeOfEducation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertStudies_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ExpertSubscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertSubscriptions_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TimeOfDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    DayId = table.Column<long>(type: "bigint", nullable: false),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_TimeOfDays_TBL_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "TBL_Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_TimeOfDays_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

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

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_TBL_ExpertExperiences_ExpertInformationId",
                table: "TBL_ExpertExperiences",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertImages_ExpertInformationId",
                table: "TBL_ExpertImages",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertInformations_SpecialtyId",
                table: "TBL_ExpertInformations",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertMemberships_ExpertInformationId",
                table: "TBL_ExpertMemberships",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertPrizes_ExpertInformationId",
                table: "TBL_ExpertPrizes",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertStudies_ExpertInformationId",
                table: "TBL_ExpertStudies",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ExpertSubscriptions_ExpertInformationId",
                table: "TBL_ExpertSubscriptions",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_AppointmentId",
                table: "TBL_TimeOfDays",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_DayId",
                table: "TBL_TimeOfDays",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TimeOfDays_ExpertInformationId",
                table: "TBL_TimeOfDays",
                column: "ExpertInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TBL_ExpertExperiences");

            migrationBuilder.DropTable(
                name: "TBL_ExpertImages");

            migrationBuilder.DropTable(
                name: "TBL_ExpertMemberships");

            migrationBuilder.DropTable(
                name: "TBL_ExpertPrizes");

            migrationBuilder.DropTable(
                name: "TBL_ExpertStudies");

            migrationBuilder.DropTable(
                name: "TBL_ExpertSubscriptions");

            migrationBuilder.DropTable(
                name: "TBL_TimeOfDays");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TBL_Appointments");

            migrationBuilder.DropTable(
                name: "TBL_Days");

            migrationBuilder.DropTable(
                name: "TBL_ConsumersInformations");

            migrationBuilder.DropTable(
                name: "TBL_ExpertInformations");

            migrationBuilder.DropTable(
                name: "TBL_Specialties");
        }
    }
}
