using Data.Dto_s;
using Data.Entities;
using Data.Repositories;
using Data.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace Application
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
                });
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
             {
                 options.MinimumSameSitePolicy = SameSiteMode.None;
                 options.HttpOnly = HttpOnlyPolicy.None;
                 options.Secure = CookieSecurePolicy.Always;

             });

            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddFluentValidation(x => { x.RegisterValidatorsFromAssemblyContaining<MyUserDto>(); });
            builder.Services.AddFluentValidation(x => { x.RegisterValidatorsFromAssemblyContaining<Cycle>(); });

            string connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
               options.SlidingExpiration = true;
               options.AccessDeniedPath = "/Forbidden/";
               options.LoginPath = "/Account/LoginPage";
           });

            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<IValidator<LoginDto>, LoginUserDtoValidator>();
            builder.Services.AddScoped<IValidator<MyUserDto>, MyUserValidator>();
            builder.Services.AddScoped<IValidator<Course>, CourseValidator>();
            builder.Services.AddScoped<IValidator<Cycle>, CycleValidator>();
            builder.Services.AddScoped<IValidator<Data.Entities.Service>, ServiceValidator>();
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<RoleRepository>();
            builder.Services.AddScoped<ServiceRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<CoursesRepository>();
            builder.Services.AddScoped<CoursesService>();
            builder.Services.AddScoped<CyclesRepository>();
            builder.Services.AddScoped<CyclesService>();
            builder.Services.AddScoped<EnrollmentsRepository>();
            builder.Services.AddScoped<EnrollmentsService>();
            builder.Services.AddScoped<AttendanceRepository>();
            builder.Services.AddScoped<AttendanceService>();
            builder.Services.AddScoped<StatusRepository>();
            builder.Services.AddScoped<StatusService>();
            builder.Services.AddScoped<ServiceService>();
            builder.Services.AddScoped<ServiceRepository>();
            builder.Services.AddScoped<PhotoRepository>();
            builder.Services.AddScoped<PhotoService>();
            builder.Services.AddScoped<MessageRepository>();
            builder.Services.AddScoped<MessageService>();
            builder.Services.AddScoped<ReservationRepository>();
            builder.Services.AddScoped<ReservationService>();
            builder.Services.AddScoped<ReservationSlotsService>();
            builder.Services.AddScoped<ReservationSlotsRepository>();
            builder.Services.AddScoped<UserReservationSlotsRepository>();
            builder.Services.AddScoped<UserReservationSlotsService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHostedService(provider =>
            {
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
                return new ReminderService(scopeFactory);
            });

            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseHttpsRedirection();

            app.UseCors("CORSPolicy");

            app.Run();
        }
    }
}