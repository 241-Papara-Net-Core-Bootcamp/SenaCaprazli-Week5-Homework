using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaparaThirdWeek.Api.Filters;
using PaparaThirdWeek.Api.Middlewares;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Concretes;
using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.Concretes;
using PaparaThirdWeek.Services.Configurations;
using PaparaThirdWeek.Services.MappingProfile;
using System.Text;
using Hangfire;

namespace PaparaThirdWeek.Api
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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaparaThirdWeek.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            services.AddAutoMapper(typeof(MappingProfile)); //Sadece olu�turdu�umuz MappingProfilini register eder.
            services.AddDbContext<PaparaAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICompanyService, CompanyServices>();
            services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>));
            services.AddTransient(typeof(ICacheService), typeof(CacheService));
            services.AddTransient(typeof(IUserService), typeof(UserService));

            //Farkl� mapper profilleri eklenirse hepsi taray�p bulup register eder.
            //  services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Reflection Kullan�m�

            // Attribue olarak ekledi�in actionda �al���r
            services.AddScoped<ValidationFilterAttribute>();

            services.AddTransient<ITokenServices, TokenServices>();

            //T�m actionlar i�in filter register y�ntemi. Global olarak yapar.
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(ValidationFilterAttribute));
            //});

            services.AddMemoryCache();
            services.Configure<CacheConfiguration>(Configuration.GetSection("CacheConfiguration"));
            services.AddTransient<CacheService>();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaparaThirdWeek.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseExceptionMiddleware();

            //app.ConfigureExceptionHandler(); // Lamda exp. ile Built-in ExceptionMiddleware
            //extension olarak kulland�k. Exception Handle yapt�k.

            app.UseAuthentication(); // JWT i�in gerekli olan middleware

            //app.UseResponseCaching(); Response Cache aktif eden middleware
            app.UseHangfireDashboard("/jobs");

        }
    }
}
