using GenreUpdateService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.Contract;
using PG.Services.Contracts.Helpers;
using PG.Services.Helpers;
using PG.Web.Middleware;
using PG.Web.Services;
using Serilog;
using System;
using System.Text;

namespace PG.Web
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
            services.AddControllersWithViews().AddNewtonsoftJson
                (x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            // configure strongly typed settings objects
            var appSettingsSection = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddScoped<IDeezerAPIService, DeezerAPIService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IBingMapsAPIService, BingMapsAPIService>();
            services.AddScoped<IPixabayService, PixabayService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUserService, UserService>();
            services.AddHostedService<GenreHostedService>();

            services.AddHttpClient<IHttpDeezerClientService, HttpDeezerClientService>(client =>
            {
                client.BaseAddress = new Uri("https://api.deezer.com/");
            });

            services.AddHttpClient<IHttpPixabayClientService, HttpPixabayClientService>(client =>
            {
                client.BaseAddress = new Uri("https://pixabay.com/");
            });

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<PGDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(config =>
            {
                //config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PGDbContext>(options => options.UseSqlServer(connectionString));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RidePal API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        Array.Empty<string>()
                    }
                });
            });
            services.AddRazorPages();
        } 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseStaticFiles();
            
            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.DefaultModelsExpandDepth(-1);
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
