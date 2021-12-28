﻿// <auto-generated />
using System;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CR.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211216104141_AddedGenderTypeToConsumerInformation")]
    partial class AddedGenderTypeToConsumerInformation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CR.DataAccess.Entities.Appointments.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ConsumerInformationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerInformationId");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_Appointments");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertAvailabilities.Day", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_Days");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertAvailabilities.TimeOfDay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AppointmentId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("DayId")
                        .HasColumnType("bigint");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<int>("Finish")
                        .HasColumnType("int");

                    b.Property<int>("Start")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("DayId");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_TimeOfDays");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertExperience", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FinishDate_String")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartDate_String")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_ExpertExperiences");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Src")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_ExpertImages");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertMembership", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_ExpertMemberships");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertPrize", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<string>("PrizeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_ExpertPrizes");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertStudy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DegreeOfEducation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndDate_String")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<string>("University")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_ExpertStudies");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertSubscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<string>("SubscriptionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.ToTable("TBL_ExpertSubscriptions");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.IndividualInformations.ConsumerInfromation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthDate_String")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ConsumerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("IconSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TBL_ConsumersInformations");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthDate_String")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClinicAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClinicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("IconSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFreeOfCharge")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TBL_ExpertInformations");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Roles.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Specialties.ExpertSpecialty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<long>("SpecialtyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ExpertInformationId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("TBL_ExpertSpecialties");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Specialties.Specialty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TBL_Specialties");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ConsumerInformationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<long?>("ExpertInformationId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("UserFlag")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerInformationId")
                        .IsUnique()
                        .HasFilter("[ConsumerInformationId] IS NOT NULL");

                    b.HasIndex("ExpertInformationId")
                        .IsUnique()
                        .HasFilter("[ExpertInformationId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Appointments.Appointment", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ConsumerInfromation", "ConsumerInformation")
                        .WithMany("ConsumerAppointments")
                        .HasForeignKey("ConsumerInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertAppointments")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConsumerInformation");

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertAvailabilities.Day", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("Days")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertAvailabilities.TimeOfDay", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.Appointments.Appointment", "Appointment")
                        .WithMany("TimeOfDays")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CR.DataAccess.Entities.ExpertAvailabilities.Day", "Day")
                        .WithMany("TimeOfDays")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("TimeOfDays")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Day");

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertExperience", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertExperiences")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertImage", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertImages")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertMembership", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertMemberships")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertPrize", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertPrizes")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertStudy", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertStudies")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertInformations.ExpertSubscription", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertSubscriptions")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Specialties.ExpertSpecialty", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithMany("ExpertSpecialties")
                        .HasForeignKey("ExpertInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CR.DataAccess.Entities.Specialties.Specialty", "Specialty")
                        .WithMany("EspertSpecialties")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertInformation");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Users.User", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ConsumerInfromation", "ConsumerInfromation")
                        .WithOne("Consumer")
                        .HasForeignKey("CR.DataAccess.Entities.Users.User", "ConsumerInformationId");

                    b.HasOne("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", "ExpertInformation")
                        .WithOne("Expert")
                        .HasForeignKey("CR.DataAccess.Entities.Users.User", "ExpertInformationId");

                    b.Navigation("ConsumerInfromation");

                    b.Navigation("ExpertInformation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CR.DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("CR.DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Appointments.Appointment", b =>
                {
                    b.Navigation("TimeOfDays");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.ExpertAvailabilities.Day", b =>
                {
                    b.Navigation("TimeOfDays");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.IndividualInformations.ConsumerInfromation", b =>
                {
                    b.Navigation("Consumer");

                    b.Navigation("ConsumerAppointments");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.IndividualInformations.ExpertInformation", b =>
                {
                    b.Navigation("Days");

                    b.Navigation("Expert");

                    b.Navigation("ExpertAppointments");

                    b.Navigation("ExpertExperiences");

                    b.Navigation("ExpertImages");

                    b.Navigation("ExpertMemberships");

                    b.Navigation("ExpertPrizes");

                    b.Navigation("ExpertSpecialties");

                    b.Navigation("ExpertStudies");

                    b.Navigation("ExpertSubscriptions");

                    b.Navigation("TimeOfDays");
                });

            modelBuilder.Entity("CR.DataAccess.Entities.Specialties.Specialty", b =>
                {
                    b.Navigation("EspertSpecialties");
                });
#pragma warning restore 612, 618
        }
    }
}
