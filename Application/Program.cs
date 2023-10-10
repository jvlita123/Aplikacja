using Service;
using Service.Dto_s;
using Service.Entities;
using Service.Repositories;
using Service.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Services;
using System;
using System.Text;

namespace Application
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
            var authenticationSettings = new AuthenticationSettings();

			builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

			builder.Services.AddSingleton(authenticationSettings);
			builder.Services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = "Bearer";
				option.DefaultScheme = "Bearer";
				option.DefaultChallengeScheme = "Bearer";
			}).AddJwtBearer(cfg =>
			{
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				cfg.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = authenticationSettings.JwtIssuer,
					ValidAudience = authenticationSettings.JwtIssuer,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
				};
			});

            builder.Services.AddControllersWithViews();
			//builder.Services.AddFluentValidation(); 
			builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

            string connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
			builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<RoleService>();
			builder.Services.AddScoped<RoleRepository>();
			builder.Services.AddScoped<ReservationRepository>();
			builder.Services.AddScoped<ReservationService>();
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
			builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();

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