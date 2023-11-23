using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json.Serialization.Metadata;

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
                   // User1 = firstUser,
                   //User2 = secondUser,
                    Text = text,
                };
                _messageRepository.AddAndSaveChanges(message);
              //  secondUser.Messages.Add(message);
           //     firstUser.Messages.Add(message);
                _userRepository.UpdateAndSaveChanges(secondUser);
                _userRepository.UpdateAndSaveChanges(firstUser);

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
            List<User> users = new List<User>();
            List<Message> messages = _messageRepository.GetAll().Where(x => x.UserId2 == id || x.UserId == id).ToList();

            foreach (var message in messages)
            {
                message.User2 = _userRepository.GetAll().Where(x => x.Id == message.UserId2).FirstOrDefault();
                message.User1 = _userRepository.GetAll().Where(x => x.Id == message.UserId).FirstOrDefault();

                if (!users.Contains(message.User2) && message.UserId2 != id)
                {
                    users.Add(message.User2);
                }
                else if (!users.Contains(message.User1) && message.UserId != id)
                {
                    users.Add(message.User1);
                }
            }
            return users;
        }

        public List<Message> GetConversation(int firstUserId, int secondUserId)
        {
            List<Message> messages = _messageRepository.GetAll().Where(x => (x.UserId == firstUserId && x.UserId2 == secondUserId) || (x.UserId == secondUserId && x.UserId2 == firstUserId)).ToList();

            foreach (var message in messages)
            {
                message.User1 = _userRepository.GetAll().Where(x => x.Id == message.UserId).FirstOrDefault();
                message.User2 = _userRepository.GetAll().Where(x => x.Id == message.UserId2).FirstOrDefault();
            }
            return messages;
        }
    }
}
