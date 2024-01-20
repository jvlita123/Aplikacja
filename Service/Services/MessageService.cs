using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class MessageService
    {
        private readonly MessageRepository _messageRepository;
        private readonly UserRepository _userRepository;

        public MessageService(MessageRepository messageRepository, UserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public List<Message> GetAll()
        {
            List<Message> messages = _messageRepository.GetAll().ToList();

            return messages;
        }

        public Message Add(Message message)
        {
            Message? newMessage = _messageRepository.AddAndSaveChanges(message);

            return newMessage;
        }

        public void Update(Message message)
        {
            _messageRepository.UpdateAndSaveChanges(message);
        }

        public Message? SendMessage(int firstUserId, int secondUserId, string text)
        {
            User firstUser = _userRepository.GetById(firstUserId);
            User secondUser = _userRepository.GetById(secondUserId);

            if (firstUser != null && secondUser != null)
            {
                var message = new Message()
                {
                    UserId = firstUserId,
                    UserId2 = secondUserId,
                    Text = text,
                    IsNew = true
                };

                _messageRepository.AddAndSaveChanges(message);
                return message;
            }
            else
            {
                return null;
            }
        }

        public List<Message> GetUserMessages(int id)
        {
            List<Message> messages = _messageRepository.GetAll().Where(x => x.UserId2 == id).ToList();
            return messages;
        }

        public List<Message> GetSentMessages(int id)
        {
            List<Message> messages = _messageRepository.GetAll().Where(x => x.UserId == id).ToList();
            return messages;
        }

        public List<User> GetUserConversations(int id)
        {
            var users = new List<User>();
            if (_userRepository.GetById(id) == null)
            {
                return users;
            }

            var messages = _messageRepository.GetAll().Include(x => x.User1).ThenInclude(x => x.Photos).Where(x => x.UserId2 == id || x.UserId == id).ToList();

            var user2Ids = GetUserMessages(id).Select(x => x.UserId).ToList();
            var user1Ids = GetSentMessages(id).Select(x => x.UserId2).ToList();

            var distinctUserIds = user2Ids.Union(user1Ids).Distinct().ToList();
            var allUsers = _userRepository.GetAll().Include(x => x.Photos).Include(x => x.Role)
                .Include(x => x.Messages).ToList();
            foreach (var userId in distinctUserIds)
            {
                User user = allUsers.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public List<Message> GetConversation(int firstUserId, int secondUserId)
        {
            List<Message> messages = _messageRepository.GetAll().Where(x => (x.UserId == firstUserId && x.UserId2 == secondUserId) || (x.UserId == secondUserId && x.UserId2 == firstUserId)).ToList();

            foreach (var message in messages)
            {
                if (message.UserId != firstUserId || (message.UserId == firstUserId && message.UserId2 == firstUserId))
                {
                    message.IsNew = false;
                }
            }
            _messageRepository.UpdateRangeAndSaveChanges(messages);

            return messages;
        }
    }
}