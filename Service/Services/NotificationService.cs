using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class NotificationService
    {
        public NotificationService() { }

        public static async Task HandleUserNotification(User user, string messageText, MessageRepository _messageRepository, User admin)
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
