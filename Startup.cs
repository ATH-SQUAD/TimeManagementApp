using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeManagementApp.App.Auth;
using TimeManagementApp.App.DataProviders;
using TimeManagementApp.App.Interfaces;
using TimeManagementApp.App.Services;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Data.EFCore;
using TimeManagementApp.Database.Models;
using MimeKit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace TimeManagementApp
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
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddScoped<IEmailSender, EmailSender>();

            // Configure database context.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });

            services.AddScoped<EfCoreConnectionsRepo>();
            services.AddScoped<EfCoreMongoConnectionsRepo>();
            services.AddScoped<EfCoreDailyRepo>();
            services.AddScoped<EfCoreVacationRepo>();

            // Configure identity system for registering, logging and authenticating users.
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                options.Stores.MaxLengthForKeys = 128;
            })  .AddEntityFrameworkStores<AppDbContext>()
                .AddRoles<ApplicationRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                 .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            // All pages except those with attribute "[AllowAnonymous]" are authorized and secured.
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(240);
            });
            services.AddMemoryCache();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/TimeManagement/Logowanie";
                options.AccessDeniedPath = "/Errors/403";
            });

            // Configure email sender for password reminder.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Manage", policy => policy.RequireClaim("Zarz¹dzanie u¿ytkownikami"));
            });

            //Configure Email Sender for password reminder.
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/TimeManagement/Start", "");
                options.Conventions.AuthorizeFolder("/TimeManagement/Administracja");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/errors/Error500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("/Errors/{0}");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            RolesService.CreateRoles(serviceProvider).Wait();
            RolesService.CreateRootUser(serviceProvider, Configuration).Wait();
        }
    }
}
