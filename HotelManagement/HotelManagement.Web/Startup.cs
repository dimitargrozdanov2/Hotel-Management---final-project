using AutoMapper;
using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.Web.Areas.Management.Hubs;
using HotelManagement.Web.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>() // TODO: Might remove it
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IUserManagerWrapper, UserManagerWrapper>();
            services.AddScoped<IRoleManagerWrapper, RoleManagerWrapper>();
            services.AddScoped<ILogbookService, LogbookService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSignalR();

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.CustomExceptionHandling();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(
                routes =>
                {
                    routes.MapHub<NoteHub>("/notesHub");
                });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "administration",
                    template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "management",
                    template: "{area:exists}/{controller=Manager}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "moderation",
                    template: "{area:exists}/{controller=Moderator}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}