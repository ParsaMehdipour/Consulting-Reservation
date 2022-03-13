using CR.Core.DTOs.FinancialTransactions;
using CR.Core.Services.Implementations.AboutUs;
using CR.Core.Services.Implementations.Appointment;
using CR.Core.Services.Implementations.BlogCategories;
using CR.Core.Services.Implementations.Blogs;
using CR.Core.Services.Implementations.ChatMessages;
using CR.Core.Services.Implementations.ChatUsers;
using CR.Core.Services.Implementations.Comments;
using CR.Core.Services.Implementations.CommissionAndDiscounts;
using CR.Core.Services.Implementations.Consumers;
using CR.Core.Services.Implementations.ContactUs;
using CR.Core.Services.Implementations.Days;
using CR.Core.Services.Implementations.ExpertAvailabilities;
using CR.Core.Services.Implementations.ExpertImages;
using CR.Core.Services.Implementations.Experts;
using CR.Core.Services.Implementations.Factors;
using CR.Core.Services.Implementations.Favorites;
using CR.Core.Services.Implementations.FinancialTransactions;
using CR.Core.Services.Implementations.Images;
using CR.Core.Services.Implementations.Rules;
using CR.Core.Services.Implementations.Specialites;
using CR.Core.Services.Implementations.Statistics;
using CR.Core.Services.Implementations.Timings;
using CR.Core.Services.Implementations.Users;
using CR.Core.Services.Implementations.Wallet;
using CR.Core.Services.Interfaces.AboutUs;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.BlogCategories;
using CR.Core.Services.Interfaces.Blogs;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.Comments;
using CR.Core.Services.Interfaces.CommissionAndDiscounts;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.ContactUs;
using CR.Core.Services.Interfaces.Days;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.Core.Services.Interfaces.ExpertImages;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Factors;
using CR.Core.Services.Interfaces.Favorites;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Rules;
using CR.Core.Services.Interfaces.Specialites;
using CR.Core.Services.Interfaces.Statistics;
using CR.Core.Services.Interfaces.Timings;
using CR.Core.Services.Interfaces.Users;
using CR.Core.Services.Interfaces.Wallet;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Roles;
using CR.DataAccess.Entities.Users;
using CR.Presentation.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersianTranslation.Identity;
using System;

namespace CR.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSignalR();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Role>(options =>
             {
                 options.User.RequireUniqueEmail = false;

                 options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";

                 options.SignIn.RequireConfirmedAccount = false;
                 options.Password.RequiredUniqueChars = 0;

                 options.Password.RequiredLength = 8;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireDigit = false;


                 //Lockout Setting
                 options.Lockout.MaxFailedAccessAttempts = 3;
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(10);

                 //SignIn Setting
                 //options.SignIn.RequireConfirmedAccount = false;
                 options.SignIn.RequireConfirmedEmail = false;
                 options.SignIn.RequireConfirmedPhoneNumber = false;
             })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.ConfigureApplicationCookie(options =>
            {
                // options.ExpireTimeSpan = 14 days
                options.AccessDeniedPath = "/Account/AccessDenied";
                //options.Cookie.Name = "IdentityProj";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddAuthorization();

            services.AddHttpContextAccessor();

            #region Service Injections 
            //Users
            services.AddScoped<IRegisterAsExpertService, RegisterAsExpertService>();
            services.AddScoped<IGetUserFlagService, GetUserFlagService>();
            services.AddScoped<IRegisterExpertFromAdminService, RegisterExpertFromAdminService>();
            services.AddScoped<IGetAdminDetailsService, GetAdminDetailsService>();
            services.AddScoped<IEditAdminDetailsService, EditAdminDetailsService>();
            //Experts
            services.AddScoped<IGetAllExpertsService, GetAllExpertsService>();
            services.AddScoped<IChangeExpertStatusService, ChangeExpertStatusService>();
            services.AddScoped<IGetExpertDetailsForProfileService, GetExpertDetailsForProfileService>();
            services.AddScoped<IGetExpertDetailsForAdminService, GetExpertDetailsForAdminService>();
            services.AddScoped<IEditExpertDetailsFromAdminService, EditExpertDetailsFromAdminService>();
            services.AddScoped<IGetLatestExpertsForAdminService, GetLatestExpertsForAdminService>();
            services.AddScoped<IGetExpertsForSiteService, GetExpertsForSiteService>();
            services.AddScoped<IGetExpertDetailsForSiteService, GetExpertDetailsForSiteService>();
            services.AddScoped<IGetExpertsForPresentationService, GetExpertsForPresentationService>();
            services.AddScoped<IGetExpertDetailsForPartialService, GetExpertDetailsForPartialService>();
            services.AddScoped<IEditBasicExpertDetailsService, EditBasicExpertDetailsService>();
            services.AddScoped<IEditAdvancedExpertDetailsService, EditAdvancedExpertDetailsService>();
            services.AddScoped<IGetExpertCallUsesService, GetExpertCallUsesService>();
            //Timings
            services.AddScoped<IGetTimingsForDropDownService, GetTimingsForDropDownService>();
            services.AddScoped<IAddNewTimingService, AddNewTimingService>();
            services.AddScoped<IGetAllTimingsForAdminService, GetAllTimingsForAdminService>();
            services.AddScoped<IRemoveTimingService, RemoveTimingService>();
            services.AddScoped<IGetAvailableTimingsService, GetAvailableTimingsService>();
            //ExpertAvailabilities
            services.AddScoped<IAddDayService, AddDayService>();
            services.AddScoped<IGetDaysService, GetDaysService>();
            services.AddScoped<IGetDayDetailsService, GetDayDetailsService>();
            services.AddScoped<IAddTimeOfDayService, AddTimeOfDayService>();
            services.AddScoped<IRemoveTimeOfDayService, RemoveTimeOfDayService>();
            services.AddScoped<IGetExpertDetailsForReservationService, GetExpertDetailsForReservationService>();
            services.AddScoped<IGetThisDateExpertDetailsForReservationService, GetThisDateExpertDetailsForReservationService>();
            services.AddScoped<IGetExpertAvailabilitiesForReservationService, GetExpertAvailabilitiesForReservationService>();
            services.AddScoped<IGetTimeOfDayPriceForReservationService, GetTimeOfDayPriceForReservationService>();
            services.AddScoped<IEditDayDetailsService, EditDayDetailsService>();
            //Consumers
            services.AddScoped<IGetAllConsumersService, GetAllConsumersService>();
            services.AddScoped<IGetConsumerDetailsForProfileService, GetConsumerDetailsForProfileService>();
            services.AddScoped<IAddConsumerDetailsService, AddConsumerDetailsService>();
            services.AddScoped<IEditConsumerDetailsService, EditConsumerDetailsService>();
            services.AddScoped<IRegisterConsumerFromAdminService, RegisterConsumerFromAdminService>();
            services.AddScoped<IGetConsumerDetailsForAdminService, GetConsumerDetailsForAdminService>();
            services.AddScoped<IGetLatestConsumersForAdminService, GetLatestConsumersForAdminService>();
            services.AddScoped<IEditConsumerDetailsFromAdminService, EditConsumerDetailsFromAdminService>();
            services.AddScoped<IGetConsumersForExpertPanelService, GetConsumersForExpertPanelService>();
            services.AddScoped<IGetTodayConsumersForExpertDashboardService, GetTodayConsumersForExpertDashboardService>();
            services.AddScoped<IGetIncomingConsumersForExpertDashboardService, GetIncomingConsumersForExpertDashboardService>();
            services.AddScoped<IGetConsumerDetailsForPartialService, GetConsumerDetailsForPartialService>();
            //Specialites
            services.AddScoped<IAddNewSpecialityService, AddNewSpecialityService>();
            services.AddScoped<IEditSpecialityService, EditSpecialityService>();
            services.AddScoped<IRemoveSpecialityService, RemoveSpecialityService>();
            services.AddScoped<IGetAllSpecialitiesService, GetAllSpecialitiesService>();
            services.AddScoped<IGetSpecialtiesForExpertProfileDropDownService, GetSpecialtiesForExpertProfileDropDownService>();
            services.AddScoped<IGetSpecialitiesForPresentationService, GetSpecialitiesForPresentationService>();
            services.AddScoped<IGetSpecialitiesForSearchService, GetSpecialitiesForSearchService>();
            //Appointments
            services.AddScoped<IAddAppointmentService, AddAppointmentService>();
            services.AddScoped<IGetAllAppointmentsForAdminPanelService, GetAllAppointmentsForAdminPanelService>();
            //services.AddScoped<IChangeAppointmentStatusServiceBool, ChangeAppointmentStatusServiceBool>();
            services.AddScoped<IGetAllAppointmentsForConsumerPanelService, GetAllAppointmentsForConsumerPanelService>();
            services.AddScoped<IGetAllAppointmentsForExpertPanelService, GetAllAppointmentsForExpertPanelService>();
            services.AddScoped<IGetConsumerAppointmentsForExpertPanelService, GetConsumerAppointmentsForExpertPanelService>();
            services.AddScoped<IGetAppointmentDetailsForExpertPanelService, GetAppointmentDetailsForExpertPanelService>();
            services.AddScoped<IGetAppointmentsForAdminDashboardService, GetAppointmentsForAdminDashboardService>();
            services.AddScoped<IChangeAppointmentStatusService, ChangeAppointmentStatusService>();
            //Images
            services.AddScoped<IImageUploaderService, ImageUploaderService>();
            //ExpertImages
            services.AddScoped<IRemoveExpertImagesService, RemoveExpertImagesService>();
            //Statistics
            services.AddScoped<IGetStatisticsForAdminPanelService, GetStatisticsForAdminPanelService>();
            services.AddScoped<IGetStatisticsForExpertPanelService, GetStatisticsForExpertPanelService>();
            //CommissionAndDiscounts
            services.AddScoped<IGetAllCommissionAndDiscountsForAdminService, GetAllCommissionAndDiscountsForAdminService>();
            services.AddScoped<IAddNewCommissionAndDiscountService, AddNewCommissionAndDiscountService>();
            services.AddScoped<IGetCommissionAndDiscountDetailsForAdminService, GetCommissionAndDiscountDetailsForAdminService>();
            services.AddScoped<IEditCommissionAndDiscountService, EditCommissionAndDiscountService>();
            //Factors
            services.AddScoped<IGetFactorDetailsService, GetFactorDetailsService>();
            services.AddScoped<IGetAllFactorsForAdminPanelService, GetAllFactorsForAdminPanelService>();
            services.AddScoped<IGetAllFactorsForExpertPanelService, GetAllFactorsForExpertPanelService>();
            services.AddScoped<IUpdateFactorRefIdService, UpdateFactorRefIdService>();
            services.AddScoped<IUpdateFactorSaleReferenceIdService, UpdateFactorSaleReferenceIdService>();
            services.AddScoped<IUpdateFactorCartHolderPanService, UpdateFactorCartHolderPanService>();
            services.AddScoped<IUpdateFactorStatusService, UpdateFactorStatusService>();
            //BlogCategories
            services.AddScoped<IGetBlogCategoriesForAdminPanelService, GetBlogCategoriesForAdminPanelService>();
            services.AddScoped<IAddNewBlogCategoryService, AddNewBlogCategoryService>();
            services.AddScoped<IDeleteBlogCategoryService, DeleteBlogCategoryService>();
            services.AddScoped<IGetBlogCategoryDetailsService, GetBlogCategoryDetailsService>();
            services.AddScoped<IEditBlogCategoryDetailsService, EditBlogCategoryDetailsService>();
            services.AddScoped<IGetBlogCategoriesForDropdownService, GetBlogCategoriesForDropdownService>();
            services.AddScoped<IGetBlogCategoriesForSideBarService, GetBlogCategoriesForSideBarService>();
            //Blogs
            services.AddScoped<IGetBlogsForAdminPanelService, GetBlogsForAdminPanelService>();
            services.AddScoped<IAddNewBlogService, AddNewBlogService>();
            services.AddScoped<IDeleteBlogService, DeleteBlogService>();
            services.AddScoped<IGetBlogDetailsService, GetBlogDetailsService>();
            services.AddScoped<IEditBlogFromAdminService, EditBlogFromAdminService>();
            services.AddScoped<IGetBlogsForExpertPanelService, GetBlogsForExpertPanelService>();
            services.AddScoped<IGetLatestBlogsForSiteService, GetLatestBlogsForSiteService>();
            services.AddScoped<IGetBlogDetailsForPresentationService, GetBlogDetailsForPresentationService>();
            services.AddScoped<IGetBlogsForPresentationService, GetBlogsForPresentationService>();
            //FinancialTransactions
            services.AddScoped<IGetFinancialTransactionsForAdminService, GetFinancialTransactionsForAdminService>();
            services.AddScoped<IGetConsumerFinancialTransactionsService, GetConsumerFinancialTransactionsService>();
            services.AddScoped<IAddChargeWalletFinancialTransactionService, AddChargeWalletFinancialTransactionService>();
            services.AddScoped<IUpdateFinancialTransactionsRefIdService, UpdateFinancialTransactionsRefIdService>();
            services.AddScoped<IGetFinancialTransactionForVerifyService, GetFinancialTransactionForVerifyService>();
            services.AddScoped<IUpdateFinancialTransactionSaleReferenceIdService, UpdateFinancialTransactionSaleReferenceIdService>();
            services.AddScoped<IUpdateFinancialTransactionCarHolderPANService, UpdateFinancialTransactionCarHolderPAN>();
            services.AddScoped<IUpdateFinancialTransactionStatusService, UpdateFinancialTransactionStatusService>();
            services.AddScoped<IAddPaymentTransactionService, AddPaymentTransactionService>();
            services.AddScoped<IGetFinancialTransactionDetailsForVerifyService, GetFinancialTransactionDetailsForVerifyService>();
            services.AddScoped<IAddPayFromWalletFinancialTransactionService, AddPayFromWalletFinancialTransactionService>();
            services.AddScoped<IAddDeclineFinancialTransactionService, AddDeclineFinancialTransactionService>();
            //Favorites
            services.AddScoped<IAddNewFavoriteService, AddNewFavoriteService>();
            services.AddScoped<IGetConsumerFavoritesService, GetConsumerFavoritesService>();
            services.AddScoped<IRemoveFromFavoritesListService, RemoveFromFavoritesListService>();
            //Comments
            services.AddScoped<IAddNewCommentService, AddNewCommentService>();
            services.AddScoped<IGetExpertCommentsForAdminPanelService, GetExpertCommentsForAdminPanelService>();
            services.AddScoped<IChangeCommentStatusService, ChangeCommentStatusService>();
            services.AddScoped<IGetExpertCommentsService, GetExpertCommentsService>();
            services.AddScoped<IGetExpertCommentsForExpertPanelService, GetExpertCommentsForExpertPanelService>();
            services.AddScoped<IAddNewReplyService, AddNewReplyService>();
            services.AddScoped<IGetBlogCommentsForAdminService, GetBlogCommentsForAdminService>();
            //Wallet
            services.AddScoped<IGetWalletBalanceService, GetWalletBalanceService>();
            //ChatUsers
            services.AddScoped<IGetExpertChatUsersService, GetExpertChatUsersService>();
            services.AddScoped<IAddNewChatUserService, AddNewChatUserService>();
            services.AddScoped<IGetConsumerChatUsersService, GetConsumerChatUsersService>();
            //ChatMessages
            services.AddScoped<IGetChatMessagesService, GetChatMessagesService>();
            services.AddScoped<IAddNewChatMessageService, AddNewChatMessageService>();
            services.AddScoped<IAddNewVoiceMessageService, AddNewVoiceMessageService>();
            //AboutUs
            services.AddScoped<IGetAboutUsContentService, GetAboutUsContentService>();
            services.AddScoped<IAddAboutUsService, AddAboutUsService>();
            services.AddScoped<IEditAboutUsService, EditAboutUsService>();
            //Rules
            services.AddScoped<IGetRulesContentService, GetRulesContentService>();
            services.AddScoped<ICreateRuleService, CreateRuleService>();
            services.AddScoped<IEditRuleService, EditRuleService>();
            //ContactUs
            services.AddScoped<IGetContactUsContentService, GetContactUsContentService>();
            services.AddScoped<IAddContactUsContentService, AddContactUsContentService>();
            services.AddScoped<IEditContactUsContentService, EditContactUsContentService>();
            services.AddScoped<IGetContactUsForAdminPanelService, GetContactUsForAdminPanelService>();
            services.AddScoped<IAddNewContactUsService, AddNewContactUsService>();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<SiteChatHub>("/chathub");
            });
        }
    }
}
