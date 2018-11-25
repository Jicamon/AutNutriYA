using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutNutriYA.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutNutriYA
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services
                //.AddDefaultIdentity<IdentityUser>()
                //.AddRoles<IdentityRole>()
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            
            //services.AddIdentity<PacientesRepository, NutriologosRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
                            , IHostingEnvironment env
                            , UserManager<IdentityUser> uManager
                            , RoleManager<IdentityRole> rManager
                            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            InitIdentity(uManager, rManager);
        }

        public void InitIdentity(UserManager<IdentityUser> userManager
                            ,RoleManager<IdentityRole> roleManager
                            ){
            if(!roleManager.RoleExistsAsync("Admon").Result){
                var adminRole = new IdentityRole("Admin");
                var result = roleManager.CreateAsync(adminRole);

            }
            if(!roleManager.RoleExistsAsync("Nutriologo").Result){
                var NutriologoRole = new IdentityRole("Nutriologo");
                var result = roleManager.CreateAsync(NutriologoRole);

            }
            if(!roleManager.RoleExistsAsync("Paciente").Result){
                var PacienteRole = new IdentityRole("Paciente");
                var result = roleManager.CreateAsync(PacienteRole);

            }


            if (userManager.FindByEmailAsync("admin@mail.com").Result==null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Password.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }   
        

        }
    }
}
