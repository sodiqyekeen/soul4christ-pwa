using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using YourSoul4Christ.App.API.Helpers;
using YourSoul4Christ.App.API.Services;

namespace YourSoul4Christ.App.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //var configBuilder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, true)
            //    .AddJsonFile("Data\\verses.json", optional: false, reloadOnChange: true)
            //    .Build();
            Configuration = configuration;
            SwaggerOptions = new SwaggerOptions(Configuration);

        }

        public SwaggerOptions SwaggerOptions { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(option =>
            {
                option.ReturnHttpNotAcceptable = true;
            });
            #region Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(SwaggerOptions.DocName, new OpenApiInfo
                {
                    Title = SwaggerOptions.Title,
                    Version = SwaggerOptions.Version,
                    Description = SwaggerOptions.Description
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Description = "JWT Authorization header using the bearer scheme",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    In = ParameterLocation.Header
                //});
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //    {
                //        {
                //            new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                },
                //                Scheme = "apiKey",
                //                Name = "Bearer",
                //                In = ParameterLocation.Header,
                //            },
                //            new List<string>()
                //        }
                //    });
                //options.OperationFilter<AddRequiredHeaderParameters>();
            });
            #endregion

            //services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>options.UseSqlite(Configuration.GetConnectionString("DefaultSQLiteConnection")));

            //services.AddDefaultIdentity<AppUser>().AddEntityFrameworkStores<AppDbContext>();
            //services.AddIdentityServer().AddApiAuthorization<AppUser, AppDbContext>();
            //services.AddAuthentication().AddIdentityServerJwt();

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });

            services.AddResponseCaching();
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddScoped<IVerseClient, VerseClient>();
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
            app.UseCors("EnableCORS");
            app.UseAuthorization();
            app.UseResponseCaching();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });
            app.UseResponseCompression();
            app.UseSwagger(options =>
            {
                options.RouteTemplate = SwaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(SwaggerOptions.UiEndpoint, SwaggerOptions.Description);
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
