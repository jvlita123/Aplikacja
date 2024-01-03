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
    public class NotificationService 
    {

        private readonly UserRepository _userRepository;
        private readonly CoursesRepository _courseRepository;
        private readonly MessageRepository _messageRepository;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _context;

        public NotificationService(IServiceScopeFactory scopeFactory, UserRepository userRepository, CoursesRepository courseRepository, MessageRepository messageRepository, IHttpContextAccessor context)
        {
            _scopeFactory = scopeFactory;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _messageRepository = messageRepository;
            _context = context;
        }


        public static async Task HandleUserNotificationAsync(User user, string messageText, MessageRepository _messageRepository, User admin)
        {
            User firstUser = admin;
            User secondUser = user;

            if (firstUser != null && secondUser != null)
            {
                var message = new Message()
                {
                    UserId = firstUser.Id,
                    UserId2 = secondUser.Id,
                    User1 = firstUser,
                    User2 = secondUser,
                    Text = messageText,
                    IsNew = true
                };


                _messageRepository.AddAndSaveChanges(message);
            }
        }

    }
}
