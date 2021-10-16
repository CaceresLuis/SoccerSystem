using System;
using MediatR;
using System.Text;
using Core.Helpers;
using Infrastructure;
using Core.Validations;
using Shared.Exceptions;
using Core.Security.Token;
using Shared.Helpers.Image;
using Infrastructure.Models;
using Core.Security.Sesscion;
using Infrastructure.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Core.Modules.TeamModule.List;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Config Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Config MediatoR
            services.AddMediatR(typeof(ListTeamsQuery).Assembly);

            //Config Datacontex
            services.AddDbContext<DataContext>(conf =>
            {
                conf.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Inyectando Swagger
            services.AddSwaggerGen();

            services.AddControllers(opt => {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            }).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ConfigValidations>());


            //Get SecretKey of userManager Secrets, also apply in JwtGenerator
            string keySecret = "YOURSECRETKEY";
            //Config para manejo de usuarios, login, etc
            IdentityBuilder builder = services.AddIdentityCore<UserEntity>();
            IdentityBuilder identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<UserEntity, IdentityRole>>();
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<UserEntity>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            //Configuracion autenticacion                                             //Secret key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
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
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IGroupTeamsRepository, GroupTeamsRepository>();

            //Helpers
            services.AddScoped<IIMageHelper, IMageHelper>();
            services.AddScoped<IListItemHelper, ListItemHelper>();
            services.AddScoped<IResetMatchHelper, ResetMatchHelper>();

            services.AddCors(o => o.AddPolicy("corsApp", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("corsApp");
            app.UseMiddleware<MiddelwareHandler>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Usando SWagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //Personalizar
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Soccer System v1");
            });
        }
    }
}
