using ApiSkeletonPoc.DAL.DataContracts;
using ApiSkeletonPoc.Filters;
using ApiSkeletonPoc.Helpers;
using ApiSkeletonPoc.Infrastucture;
using ApiSkeletonPoc.Resolver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using ApiSkeletonPoc.Extensions;
using Amazon.mws.service.Crons;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using Hangfire.Annotations;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace ApiSkeletonPoc
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
            services.AddControllers();
            //services.AddHostedService<RequestReportJob>();
            //services.AddHostedService<DownloadRequestedReportJob>();
            services.AddSwaggerDocumentation();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false; options.Filters.Add(typeof(ValidateModelAttribute));
            });
            services.AddMvcCore().AddDataAnnotations();
            services.AddDbContext<GreenTriangleDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString
              ("GreenTriangleConnectionString")));
            DIResolver.ConfigureServices(services);
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.Configure<TokenOption>(Configuration.GetSection("TokenOption"));
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            //add jwt
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["TokenOption:Issuer"],
                        ValidAudience = Configuration["TokenOption:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            // cors
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                builder =>
                {
                    //builder.WithOrigins("http://18.133.244.131:8002", "http://18.133.244.131:8001", "http://18.133.244.131:9001",
                    //                    "http://localhost:4200"
                    //                    )
                    builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();

                });
            });
            //services.AddApplicationInsightsTelemetry();
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangFireConnectionConnectionString"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            //services.AddHangfireServer();
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerDocumention();

            app.UseHangfireDashboard("/Admin/Jobs", new DashboardOptions()
            {
                Authorization = new[] { new CustomAuthorizeFilter() }
            });
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            RecurringJob.AddOrUpdate(() => RequestReportJob.RequestReport(), Cron.Hourly());
            RecurringJob.AddOrUpdate(() => DownloadRequestedReportJob.DownloadReport(), "*/50 * * * *");
            RecurringJob.AddOrUpdate(() => GetMatchingProductsJob.GetMatchingProduct(), Cron.Daily());
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.WebRootPath, "images")),
                RequestPath = "/Images"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.WebRootPath, "images")),
                RequestPath = "/Images"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.WebRootPath, "documents")),
                RequestPath = "/Documents"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.WebRootPath, "documents")),
                RequestPath = "/Documents"
            });
            app.UseCors("MyPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware(typeof(CustomExceptionMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

        }
    }

    public class CustomAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            //var httpcontext = context.GetHttpContext();
            //return httpcontext.User.Identity.IsAuthenticated;
            return true;
        }
    }
}
