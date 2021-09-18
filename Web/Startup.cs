using System;
using MediatR;
using System.Text;
using Infrastructure;
using Core.Security.Token;
using Shared.Helpers.Image;
using Infrastructure.Models;
using Core.Security.Sesscion;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Core.Modules.TeamModule.Add;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Web
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
            services.AddSingleton(Configuration);

            //Get SecretKey of userManager Secrets
            string keySecret = Configuration["SecretKey"];

            //Config MediatoR
            services.AddMediatR(typeof(AddTeamCommand).Assembly);

            services.AddControllersWithViews();

            //Config Datacontex
            services.AddDbContext<DataContext>(conf =>
            {
                conf.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });

            //Config Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Config para manejo de usuarios, login, etc
            IdentityBuilder builder = services.AddIdentityCore<UserEntity>();
            IdentityBuilder identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<UserEntity, IdentityRole>>();

            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<UserEntity>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddIdentity<UserEntity, IdentityRole>().AddDefaultTokenProviders()
              .AddEntityFrameworkStores<DataContext>();

            //Configuracion autenticacion                                             //Secret key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });

            //Inyection of Repositories
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IGroupTeamsRepository, GroupTeamsRepository>();

            //Helpers
            services.AddScoped<IIMageHelper, IMageHelper>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
