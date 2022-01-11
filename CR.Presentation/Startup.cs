using CR.Core.Services.Impl.Consumers;
using CR.Core.Services.Impl.Images;
using CR.Core.Services.Impl.Specialites;
using CR.Core.Services.Impl.Users;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Specialites;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Roles;
using CR.DataAccess.Entities.Users;
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
using CR.Core.Services.Impl.Appointment;
using CR.Core.Services.Impl.ExpertAvailabilities;
using CR.Core.Services.Impl.Experts;
using CR.Core.Services.Impl.Statistics;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Statistics;

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


                 //Lokout Setting
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
            services.AddScoped<IEditExpertDetailsService,EditExpertDetailsService>();
            services.AddScoped<IGetExpertDetailsForAdminService, GetExpertDetailsForAdminService>();
            services.AddScoped<IEditExpertDetailsFromAdminService, EditExpertDetailsFromAdminService>();
            services.AddScoped<IGetLatestExpertsForAdminService, GetLatestExpertsForAdminService>();
            services.AddScoped<IGetExpertsForSiteService, GetExpertsForSiteService>();
            services.AddScoped<IGetExpertDetailsForSiteService, GetExpertDetailsForSiteService>();
            services.AddScoped<IGetExpertsForPresentationService, GetExpertsForPresentationService>();
            services.AddScoped<IGetExpertDetailsForPartialService, GetExpertDetailsForPartialService>();
            services.AddScoped<IEditBasicExpertDetailsService, EditBasicExpertDetailsService>();
            services.AddScoped<IEditAdvancedExpertDetailsService, EditAdvancedExpertDetailsService>();
            //ExpertAvailabilities
            services.AddScoped<IAddDayService, AddDayService>();
            services.AddScoped<IGetDaysService, GetDaysService>();
            services.AddScoped<IAddTimeOfDayService, AddTimeOfDayService>();
            services.AddScoped<IRemoveTimeOfDayService, RemoveTimeOfDayService>();
            services.AddScoped<IGetExpertDetailsForReservationService, GetExpertDetailsForReservationService>();
            services.AddScoped<IGetThisDateExpertDetailsForReservationService, GetThisDateExpertDetailsForReservationService>();
            //Consumers
            services.AddScoped<IGetAllConsumersService, GetAllConsumersService>();
            services.AddScoped<IGetConsumerDetailsForSiteService, GetConsumerDetailsForSiteService>();
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
            services.AddScoped<IEditSpecialityService,EditSpecialityService>();
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
            //Statistics
            services.AddScoped<IGetStatisticsForAdminPanelService, GetStatisticsForAdminPanelService>();
            services.AddScoped<IGetStatisticsForExpertPanelService, GetStatisticsForExpertPanelService>();
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
            });
        }
    }
}
