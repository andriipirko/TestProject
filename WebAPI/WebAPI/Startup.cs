using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
using WebApi.Database;
using WebApi.Database.Entities;
using WebAPI.Attributes;
using WebAPI.Contracts;
using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Configuration", "config.json"), optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationOptions = new AuthenticationOptions(Configuration);
            services.AddSingleton(authenticationOptions);

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUserGroupsService, UserGroupsService>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetSection("databaseConnectionString").Value, b => b.MigrationsAssembly("WebAPI")));

            services.AddIdentity<User, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var keyByteArray = Encoding.ASCII.GetBytes(authenticationOptions.Secret);
                var signingKey = new SymmetricSecurityKey(keyByteArray);
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingKey,
                    ValidAudience = authenticationOptions.Audience,
                    ValidIssuer = authenticationOptions.Issuer,
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));

            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddAuthorization()
                .AddApiExplorer();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            });
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }

    public class AuthenticationOptions
    {
        private const string SECTION_KEY = "AuthOptions";

        public AuthenticationOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(SECTION_KEY);

            Issuer = section["issuer"];
            Audience = section["audience"];
            Secret = section["secret"];
            LifeTime = int.Parse(section["lifeTime"]);
        }

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int LifeTime { get; set; }

        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
