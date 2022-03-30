using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CR.DataAccess.Migrations
{
    public partial class Init : Migration
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
                name: "TBL_AboutUs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_BlogCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowOrder = table.Column<int>(type: "int", nullable: false),
                    PictureSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_BlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_BlogCategories_TBL_BlogCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "TBL_BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ConsumersInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ContactUs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ContactUsContents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ContactUsContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Rule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Rule", x => x.Id);
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
                name: "TBL_Timings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimingType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Timings", x => x.Id);
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
                name: "TBL_Blogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowOrder = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanonicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BlogCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Blogs_TBL_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "TBL_BlogCategories",
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
                    UsePhoneCall = table.Column<bool>(type: "bit", nullable: false),
                    PhoneCallPrice = table.Column<long>(type: "bigint", nullable: false),
                    UseVoiceCall = table.Column<bool>(type: "bit", nullable: false),
                    VoiceCallPrice = table.Column<long>(type: "bigint", nullable: false),
                    UseTextCall = table.Column<bool>(type: "bit", nullable: false),
                    TextCallPrice = table.Column<long>(type: "bigint", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpertId = table.Column<long>(type: "bigint", nullable: false),
                    SpecialtyId = table.Column<long>(type: "bigint", nullable: true),
                    CommissionAndDiscountId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ExpertInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ExpertInformations_TBL_CommissionAndDiscounts_CommissionAndDiscountId",
                        column: x => x.CommissionAndDiscountId,
                        principalTable: "TBL_CommissionAndDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IconSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ImageType = table.Column<int>(type: "int", nullable: false),
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
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: true),
                    ConsumerInformationId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Factors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Factors_TBL_ConsumersInformations_ConsumerInformationId",
                        column: x => x.ConsumerInformationId,
                        principalTable: "TBL_ConsumersInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_Factors_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "TBL_ChatUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "TBL_Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentStatus = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerRecordId = table.Column<long>(type: "bigint", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Comments_TBL_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TBL_Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Favorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Favorites_TBL_ExpertInformations_ExpertInformationId",
                        column: x => x.ExpertInformationId,
                        principalTable: "TBL_ExpertInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentStatus = table.Column<int>(type: "int", nullable: false),
                    CallingType = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    ExpertInformationId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumerInformationId = table.Column<long>(type: "bigint", nullable: false),
                    TimeOfDayId = table.Column<long>(type: "bigint", nullable: false),
                    FactorId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_TBL_Appointments_TBL_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "TBL_Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: true),
                    Price_Digit = table.Column<long>(type: "bigint", nullable: false),
                    Price_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate_String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardHolderPAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleReferenceId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FactorId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_FinancialTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_FinancialTransactions_TBL_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "TBL_Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ChatUserMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageFlag = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TBL_TimeOfDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimingType = table.Column<int>(type: "int", nullable: false),
                    PhoneCallPrice = table.Column<long>(type: "bigint", nullable: false),
                    VoiceCallPrice = table.Column<long>(type: "bigint", nullable: false),
                    TextCallPrice = table.Column<long>(type: "bigint", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
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
                name: "IX_TBL_Appointments_FactorId",
                table: "TBL_Appointments",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_BlogCategories_ParentCategoryId",
                table: "TBL_BlogCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Blogs_BlogCategoryId",
                table: "TBL_Blogs",
                column: "BlogCategoryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Comments_ParentId",
                table: "TBL_Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Comments_UserId",
                table: "TBL_Comments",
                column: "UserId");

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
                name: "IX_TBL_ExpertInformations_CommissionAndDiscountId",
                table: "TBL_ExpertInformations",
                column: "CommissionAndDiscountId",
                unique: true,
                filter: "[CommissionAndDiscountId] IS NOT NULL");

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
                name: "IX_TBL_Factors_ConsumerInformationId",
                table: "TBL_Factors",
                column: "ConsumerInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Factors_ExpertInformationId",
                table: "TBL_Factors",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Favorites_ExpertInformationId",
                table: "TBL_Favorites",
                column: "ExpertInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Favorites_UserId",
                table: "TBL_Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_FinancialTransactions_FactorId",
                table: "TBL_FinancialTransactions",
                column: "FactorId");

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
                name: "TBL_AboutUs");

            migrationBuilder.DropTable(
                name: "TBL_Blogs");

            migrationBuilder.DropTable(
                name: "TBL_ChatUserMessages");

            migrationBuilder.DropTable(
                name: "TBL_Comments");

            migrationBuilder.DropTable(
                name: "TBL_ContactUs");

            migrationBuilder.DropTable(
                name: "TBL_ContactUsContents");

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
                name: "TBL_Favorites");

            migrationBuilder.DropTable(
                name: "TBL_FinancialTransactions");

            migrationBuilder.DropTable(
                name: "TBL_Rule");

            migrationBuilder.DropTable(
                name: "TBL_TimeOfDays");

            migrationBuilder.DropTable(
                name: "TBL_Timings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TBL_BlogCategories");

            migrationBuilder.DropTable(
                name: "TBL_ChatUsers");

            migrationBuilder.DropTable(
                name: "TBL_Appointments");

            migrationBuilder.DropTable(
                name: "TBL_Days");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TBL_Factors");

            migrationBuilder.DropTable(
                name: "TBL_ConsumersInformations");

            migrationBuilder.DropTable(
                name: "TBL_ExpertInformations");

            migrationBuilder.DropTable(
                name: "TBL_CommissionAndDiscounts");

            migrationBuilder.DropTable(
                name: "TBL_Specialties");
        }
    }
}
