using Hospital_Management.Areas.AssistantPortal.Services;
using Hospital_Management.Authorization;
using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital_Management
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
             {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<IdentityRole>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/User/Login";
                    options.LogoutPath = "/User/Logout";
                    options.AccessDeniedPath = "/User/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();

            #region IoC for services
            // User services DI.
            builder.Services.AddScoped(typeof(IUserServices<>), typeof(UserServices<>));

            // Repositories DI.
            builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IAssistantRepo, AssistantRepo>();
            builder.Services.AddScoped<IReservationRepo, ReservationRepo>();
            builder.Services.AddScoped<IRateRepo, RateRepo>();
            builder.Services.AddScoped<IRecordRepo, RecordRepo>();
            builder.Services.AddScoped<ISpecialityRepo, SpecialityRepo>();

            // Unit of work DI.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Home services DI.
            builder.Services.AddScoped<IHomeServices, HomeServices>();

            // Article services DI.
            builder.Services.AddScoped<IArticleServices, ArticleServices>();

            // Speciality services DI.
            builder.Services.AddScoped<ISpecialityServices, SpecialityServices>();

            // Doctor services DI.
            builder.Services.AddScoped<IDoctorServices, DoctorServices>();

            // Reservation services DI.
            builder.Services.AddScoped<IReservationServices, ReservationServices>();

            // Email services DI.
            builder.Services.AddTransient<IEmailServices, EmailServices>(); // Added as transient because it's not always needed.
            
            // Add Singleton for Confirmed Account Auth.
            builder.Services.AddScoped<IAuthorizationHandler, ConfirmedAccountHandler>();
          
            // Assistant Services DI.
            builder.Services.AddScoped<IAssistantSer, AssistantSer>();
            #endregion

            #region Authorization Policies
            builder.Services.AddAuthorization(options =>
            {
                // Add policy for confirmed account.
                options.AddPolicy("ConfirmedAccount", policy => policy.Requirements.Add(new ConfirmedAccountRequirement()));

                options.AddPolicy("LoggedInUser", policy => policy.RequireAuthenticatedUser());

                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireDoctorRole", policy => policy.RequireRole("Doctor"));
                options.AddPolicy("RequireAssistantRole", policy => policy.RequireRole("Assistant"));
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Add the following code to the Main method in Program.cs to enable routing for areas:
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}