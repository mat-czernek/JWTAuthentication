using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JWTWebApiAuth.Infrastructure;
using JWTWebApiAuth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace JWTWebApiAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; protected set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // get JWT settings from the appsettings.json file
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            // in memory database, just for tests
            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("UserDB"));

            // identity store
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            // database context
            services.AddDbContext<ContactsListDbContext>(opt => opt.UseInMemoryDatabase("DBContactList"));

            // database operations service registration
            services.AddScoped<IDatabaseProvideService, DatabaseProviderService>();

            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddScoped<ISignInManagementService, SignInManagementService>();

            services.AddScoped<IJwtManagement, JwtManagement>();

            services.AddScoped<IResultManagementService, ResultManagementService>();

            services.AddMvc();

            // JWT configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // JWT validation rules 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("JWTSettings:Issuer").Value,
                        ValidAudience = Configuration.GetSection("JWTSettings:Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWTSettings:SecretKey").Value))
                    }; 
                }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();    
        }
    }
}
