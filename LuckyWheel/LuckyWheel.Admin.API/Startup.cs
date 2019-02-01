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

namespace LuckyWheel.Admin.API
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
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ISpinService, SpinService>();


            services.AddCors();
            services.AddMvc();
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseMvc();
        }
    }
}
