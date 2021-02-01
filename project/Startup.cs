using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using project.Data;
using project.Models;
using project.Services;
using DevExpress.DashboardAspNetCore;
using DevExpress.AspNetCore;
using Microsoft.Extensions.FileProviders;
using DevExpress.DashboardWeb;


namespace project
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            FileProvider = hostingEnvironment.ContentRootFileProvider;

        }
        public static class EndpointRouteBuilderExtension { };
        public IConfiguration Configuration { get; }
        public IFileProvider FileProvider { get; }
        public static object ConnectionManager { get; internal set; }



         

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddDbContext<ParcInfoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //var connection = new System.Data.SqlClient.SqlConnection("Server=DESKTOP-DDGJARM;Database=Parc_informatique; User Id=tsi;password=Pfe@2020;Integrated Security=false");
            //connection.Open();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver())
            .AddDefaultDashboardController(configurator => {
                 configurator.SetDashboardStorage(new DashboardFileStorage(FileProvider.GetFileInfo("App_Data/Dashboards").PhysicalPath));
                 configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(Configuration));


            });

            services.AddDevExpressControls();
            //services.AddSignalR();
        }
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            // Register the DevExpress middleware.
            app.UseDevExpressControls();
            app.UseMvc(routes =>
            {   // Map dashboard routes.
                routes.MapDashboardRoute("api/dashboard");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
