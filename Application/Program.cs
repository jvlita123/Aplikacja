using Data.Dto_s;
using Data.Entities;
using Data.Repositories;
using Data.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace Application
{
    public class Program
    {
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
                    .WithOrigins("http://localhost:3000");
                });
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            //builder.Services.AddFluentValidation(); 
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

            string connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
               options.SlidingExpiration = true;
               options.AccessDeniedPath = "/Forbidden/";
           });

            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<RoleRepository>();
          //  builder.Services.AddScoped<ReservationRepository>();
         //   builder.Services.AddScoped<ReservationService>();
            builder.Services.AddScoped<ServiceRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<CategoriesRepository>();
            builder.Services.AddScoped<CategoriesService>();
            builder.Services.AddScoped<CoursesRepository>();
            builder.Services.AddScoped<CoursesService>();
            builder.Services.AddScoped<CyclesRepository>();
            builder.Services.AddScoped<CyclesService>();
            builder.Services.AddScoped<CoursesPerCycleRepository>();
            builder.Services.AddScoped<CoursesPerCycleService>();
            builder.Services.AddScoped<SurveyRepository>();
            builder.Services.AddScoped<SurveyService>();
            builder.Services.AddScoped<QuestionRepository>();
            builder.Services.AddScoped<QuestionService>();
            builder.Services.AddScoped<AnswerOptionRepository>();
            builder.Services.AddScoped<AnswerOptionService>();
            builder.Services.AddScoped<AnswerRepository>();
            builder.Services.AddScoped<AnswerService>();
            builder.Services.AddScoped<ResponseRepository>();
            builder.Services.AddScoped<ResponseService>();
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
            builder.Services.AddScoped<BlockRepository>();
            builder.Services.AddScoped<BlockService>();
            builder.Services.AddScoped<MessageRepository>();
            builder.Services.AddScoped<MessageService>();
            builder.Services.AddScoped<Reservation1Repository>();
            builder.Services.AddScoped<Reservation1Service>();
            builder.Services.AddScoped<ReservationSlotsService>();
            builder.Services.AddScoped<ReservationSlotsRepository>();
            builder.Services.AddScoped<UserReservationSlotsRepository>();
            builder.Services.AddScoped<UserReservationSlotsService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseHttpsRedirection();
            app.Run();
        }
    }
}