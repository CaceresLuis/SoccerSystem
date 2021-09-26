using Core.Helpers;
using Core.Modules.TeamModule.Add;
using Core.Modules.TeamModule.List;
using Core.Security.Sesscion;
using Core.Security.Token;
using Core.Validations;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.Exceptions;
using Shared.Helpers.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api
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
            services.AddCors(o => o.AddPolicy("corsApp", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            //Config Datacontex
            services.AddDbContext<DataContext>(conf =>
            {
                conf.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddOptions();

            //Config MediatoR
            services.AddMediatR(typeof(ListTeamsQuery).Assembly);

            services.AddControllers(opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ConfigValidations>());

            //services.AddControllers().AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<ConfigValidations>());

            //Inyectando Swagger
            services.AddSwaggerGen();

            //Get SecretKey of userManager Secrets
            string keySecret = Configuration["SecretKey"];

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
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IGroupTeamsRepository, GroupTeamsRepository>();

            //Helpers
            services.AddScoped<IIMageHelper, IMageHelper>();
            services.AddScoped<IListItemHelper, ListItemHelper>();
            services.AddScoped<IResetMatchHelper, ResetMatchHelper>();
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

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

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
