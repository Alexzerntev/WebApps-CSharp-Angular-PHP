using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LuckyWheel.DAL;
using LuckyWheel.BLL;
using LuckyWheel.Model;
using LuckyWheel.BLL.Interfaces;
using Newtonsoft.Json.Serialization;
using LuckyWheel.API.WebSockets;

namespace LuckyWheel.API
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
            //DI
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.AddTransient<IWheelService, WheelService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ISpinService, SpinService>();

            //Authentication
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password = StartUpHelper.GetPasswordOptions();

                // Lockout settings
                options.Lockout = StartUpHelper.GetLockoutOptions();

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = StartUpHelper.GetTokenValidationParameters(Configuration);
            });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

            services.AddCors();
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
            app.UseWebSockets();
            app.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
