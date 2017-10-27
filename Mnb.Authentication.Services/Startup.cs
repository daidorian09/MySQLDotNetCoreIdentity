using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mnb.Authentication.Services.DAL;
using Mnb.Authentication.Services.Identity;
using System.Text;

namespace Mnb.Authentication.Services
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
            services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;

        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = Configuration["JWT:Issuer"],
            ValidAudience = Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
        };

    });





            services.AddMvc();
            services.AddDbContext<TestAuthDBContext>(options =>
                                                     options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                                                     mySQLOptions => mySQLOptions.MigrationsAssembly("Mnb.Authentication.Services")));


            services.AddIdentity<TestUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequiredLength = 8;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = true;
            })
          .AddEntityFrameworkStores<TestAuthDBContext>()
          .AddDefaultTokenProviders();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();




            app.UseMvc();
        }
    }
}
