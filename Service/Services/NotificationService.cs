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

            var adminUser = _userRepository.GetAll().Include(u => u.Role).FirstOrDefault(x => x.Role.Name.ToLower() == "admin");

        }


        public static async Task HandleUserNotificationAsync(User user, string messageText, MessageRepository _messageRepository, User admin, IHttpContextAccessor _context)
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

                var user1 = _context.HttpContext?.User;

                if (user1 != null)
                {
                    if (message.User2.Messages.Where(x => x.IsNew == true).Count() > 0)
                    {


                        var claimsIdentity = (ClaimsIdentity)user1.Identity;

                        var existingNewMessageClaim = claimsIdentity.FindFirst("newMessage");
                        if (existingNewMessageClaim != null)
                        {
                            claimsIdentity.RemoveClaim(existingNewMessageClaim);
                        }

                        // Dodanie nowego claimu 'newMessage' z nową wartością
                        claimsIdentity.AddClaim(new Claim("newMessage", "true"));

                        // Zaktualizowanie informacji o uwierzytelnieniu
                        await _context.HttpContext.SignInAsync(_context.HttpContext.User);
                    }
                }

            }
        }

    }
}
