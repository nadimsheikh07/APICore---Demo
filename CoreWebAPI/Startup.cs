using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CoreApi.Extensions;
using Serilog;
using CoreApi.UserManagment.DataAccess;
using CoreApi.LoggerService;
using CoreApi.UserManagment.Business.Interfaces;
using CoreApi.UserManagment.Business;

namespace CoreApi.Controllers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
              .CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Enable CORS
            services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                });

            services.AddControllers();

            services.AddSwaggerGen(confg =>
            {
                confg.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWebAPI", Version = "v1" });
                confg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                confg.AddSecurityRequirement(new OpenApiSecurityRequirement {
            { new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            },
            new string[] {}
            }
            });
            });
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
            services.AddMvc();

            services.AddLocalization();
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserManagemntDbContext>(options => options.UseNpgsql(sqlConnectionString));
           // services.AddDbContext<CityManagemntDbContext>(options => options.UseNpgsql(sqlConnectionString));

            services.AddSingleton<ILoggerManager, LoggerManager>();
            //services.AddScoped<IRoleManager, RoleManager>();
            //services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IProductTypeManager, ProductTypeManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebAPI v1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();
            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
