using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;
using AutoRepairShop.Web.Data.Repositories.Classes;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers;
using AutoRepairShop.Web.Helpers.Classes;
using AutoRepairShop.Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AutoRepairShop.Web
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


            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                config.SignIn.RequireConfirmedEmail = true;
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 6;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();








            services.AddDbContext<DataContext>(config =>
            {
                config.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddTransient<SeedDb>();




            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IFuelRepository, FuelRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IZipCodeRepository, ZipCodeRepository>();
            services.AddScoped<IActiveScheduleRepository, ActiveScheduleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IDealershipRepository, DealershipRepository>();
            services.AddScoped<IServicesSuppliedRepository, ServicesSuppliedRepository>();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<IComboHelpers, ComboHelpers>();
            services.AddScoped<IScheduleDetailRepository, ScheduleDetailRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<IRepairScheduleRepository, RepairScheduleRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<IDealershipDepartmentRepository, DealershipDepartmentRepository>();




            services.AddScoped<IConverterHelper, ConverterHelper>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IDataInputHelper, DataInputHelper>();
            services.AddScoped<IMainWindowConverterHelper, MainWindowConverterHelper>();








            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Home/Main", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE2NjU5QDMxMzgyZTMyMmUzMGNTM0tiVzhha2hYR3NoL3I5UW1qelNoVWtubUo3T0wrbklkd2QxS29rNTQ9");

            

            app.UseMvc(routes =>
            {
                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
