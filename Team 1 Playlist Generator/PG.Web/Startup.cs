using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.Contract;

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


            services.AddScoped<IDeezerAPIService, DeezerAPIService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IBingMapsAPIService, BingMapsAPIService>();



            services.AddDefaultIdentity<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<PGDbContext>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PGDbContext>(options => options.UseSqlServer(connectionString));

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
            app.UseStaticFiles();

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
