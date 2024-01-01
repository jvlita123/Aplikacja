

using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Service.Services
{
    public class ReminderService : BackgroundService
    {


        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _context;

        public ReminderService(IServiceScopeFactory scopeFactory, IHttpContextAccessor context)
        {
            _scopeFactory = scopeFactory;
            this._context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
                var _courseRepository = scope.ServiceProvider.GetRequiredService<CoursesRepository>();
                var _cycleRepository = scope.ServiceProvider.GetRequiredService<CyclesRepository>();
                var _messageRepository = scope.ServiceProvider.GetRequiredService<MessageRepository>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    var coursesStartingTomorrow = _courseRepository.GetAll()
                        .Include(c => c.Cycles).Include(x=>x.Enrollments)
                        .Where(c => c.Cycles.Any(cycle => cycle.StartDate.Date == DateTime.Today.AddDays(1))).ToList();

                    foreach (var course in coursesStartingTomorrow)
                    {
                        foreach (var v in course.Cycles.Where(cycle => cycle.StartDate.Date == DateTime.Today.AddDays(1) && cycle.IsNotificationSent == false))
                        {

                            var adminUser = _userRepository.GetAll().Where(x => x.Role.Name.ToLower() == "admin").FirstOrDefault();
                            if (adminUser != null)
                            {
                                foreach (var user in course.Enrollments.Select(e => e.User))
                                {
                                    var messageText = $"Tomorrow will be the next course day: {v.Title} for the course '{course.Title}'";


                                    v.IsNotificationSent = true;
                                    _cycleRepository.Update(v);
                                    _cycleRepository.SaveChanges();

                                    NotificationService.HandleUserNotificationAsync(user, messageText, _messageRepository, adminUser, _context);
                                }
                            }
                        }
                    }

                    await Task.Delay(TimeSpan.FromDays(24), stoppingToken); // Czekaj 24 godziny
                }
            }
        }





        public static void HandleUserNotification(User user, string messageText, MessageRepository _messageRepository, User admin)
        {
            User firstUser = admin;
            User secondUser = user;

            if (firstUser != null && secondUser != null)
            {
                var message = new Message()
                {
                    UserId = firstUser.Id,
                    UserId2 = secondUser.Id,
                    Text = messageText,
                    IsNew = true
                };

                _messageRepository.AddAndSaveChanges(message);

            }
        }
    }
}