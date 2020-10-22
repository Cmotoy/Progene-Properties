using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgenesisProperties.data.DatabaseContext.ApplicationDbContext;
using ProgenesisProperties.data.DatabaseContext.AuthenticationDbContext;
using ProgenesisProperties.data.Entities;
using ProgenesisProperties.Interfaces;
using ProgenesisProperties.Services;

namespace ProgenesisProperties
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
            services.AddDbContextPool<AuthenticationDbContext>
            (Options =>Options.UseSqlServer
            (Configuration.GetConnectionString ("AuthenticationConnection"),
            sqlServerOptions => {sqlServerOptions.MigrationsAssembly ("ProgenesisProperties.data");
            }));           

            services.AddDbContextPool<ApplicationDbContext> 
            (Options => Options.UseSqlServer 
            (Configuration.GetConnectionString("ApplicationConnection"),
            sqlServerOptions => {sqlServerOptions.MigrationsAssembly ("ProgenesisProperties.data");
            }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions> (options => 
            {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase =false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.SignIn.RequireConfirmedEmail = false;
                 options.SignIn.RequireConfirmedPhoneNumber = false;
            });
                services.AddControllersWithViews();
                services.AddTransient<IAccountsService, AccountsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            MigrateDatabaseContext(svp);
            CreateDefaultRolesAndUsers(svp).GetAwaiter().GetResult();
            
        }    
        public void MigrateDatabaseContext(IServiceProvider svp)
        {
            var AuthenticationDbContext = svp.GetRequiredService<AuthenticationDbContext>();
            AuthenticationDbContext.Database.Migrate();

            var ApplicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            ApplicationDbContext.Database.Migrate();
        }
        public async Task CreateDefaultRolesAndUsers (IServiceProvider svp)        
        {
            string [] roles = new string[] {"SystemAdministrator", "Agent", "User"};
            var userEmail = "admin@ProgenPro.com";
            var userPassword = "SuperSecretPassword";

            var roleManager = svp.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
               var roleExist = await roleManager.RoleExistsAsync(role);
               if (!roleExist)
               {
                   await roleManager.CreateAsync(new IdentityRole {Name = role});
               }
            }       
            var userManager = svp.GetRequiredService<UserManager<ApplicationUser>>();
             var user = await userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                user = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "+2347060492799",
                    PhoneNumberConfirmed = true,
                
                };
                await userManager.CreateAsync(user, userPassword);
                await userManager.AddToRolesAsync(user, roles);
            }
        }   
 
    }  
}     

 
