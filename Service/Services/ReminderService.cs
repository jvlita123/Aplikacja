﻿using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReminderService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
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
                        .Include(c => c.Cycles).Include(x => x.Enrollments).ThenInclude(x => x.User)
                        .Where(c => c.Cycles.Any(cycle => cycle.StartDate.Date == DateTime.Today.AddDays(1))).ToList();

                    foreach (var course in coursesStartingTomorrow)
                    {
                        foreach (var v in course.Cycles.Where(cycle => cycle.StartDate.Date == DateTime.Today.AddDays(1) && cycle.IsNotificationSent == false).ToList())
                        {

                            var adminUser = _userRepository.GetAll().Where(x => x.Role.Name.ToLower() == "admin").FirstOrDefault();
                            if (adminUser != null)
                            {
                                foreach (var user in course.Enrollments.Select(e => e.User))
                                {
                                    var messageText = $"Tomorrow will be the next course day: {v.Title} for the course '{course.Title}'";

                                    v.IsNotificationSent = true;
                                    _cycleRepository.UpdateAndSaveChanges(v);

                                    await NotificationService.HandleUserNotification(user, messageText, _messageRepository, adminUser);
                                }
                            }
                        }
                    }
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
            }
        }
    }
}