using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystemApi
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:Secretkey"]);
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Audience"],
                    
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)                    
                };
                
            });
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryManagementSystemApi", Version = "v1" });
            });
            
            services.AddControllers().AddNewtonsoftJson();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            Assembly service = Assembly.Load("LibraryManagementSystem.DAL");
            Assembly iService = Assembly.Load("LibraryManagementSystem.IDAL");
            Assembly manager = Assembly.Load("LibraryManagementSystem.BLL");
            Assembly iManager = Assembly.Load("LibraryManagementSystem.IBLL");

            containerBuilder.RegisterAssemblyTypes(service, iService)
                .Where(t => t.FullName != null && t.FullName.EndsWith("Service") && !t.IsAbstract) //类名以service结尾，且类型不能是抽象的　
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(manager, iManager)
                .Where(t => t.FullName != null && t.FullName.EndsWith("Manager") && !t.IsAbstract) //类名以service结尾，且类型不能是抽象的　
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            containerBuilder.RegisterModule(new AutoMapperModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManagementSystemApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
