using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TMAS.DB.Context;
using TMAS.Configuration;
using TMAS.BLL.Mapper;
using TMAS.BLL.Services;
using TMAS.DAL.Repositories;
using AutoMapper;
using TMAS.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

namespace TMAS
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

            services.AddIdentity<User, Role>(
                options=>
                {
                    options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
              
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryApiScopes(Resources.GetApiScopes())
                .AddAspNetIdentity<User>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
           {
               options.Authority = "https://localhost:44324";

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = false
               };
           });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Test", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("email", "openid", "profile", "api.read");
                });
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddScoped<UserService>();
            services.AddScoped<BoardService>();
            services.AddScoped<CardService>();
            services.AddScoped<ColumnService>();
            services.AddScoped<HistoryService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<BoardRepository>();
            services.AddScoped<CardRepository>();
            services.AddScoped<ColumnRepository>();
            services.AddScoped<HistoryRepository>();
            services.AddScoped<Controllers.Base.UserParams>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TMAS.DB")));

            services.AddSwaggerGen();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
 

            app.UseIdentityServer();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}
