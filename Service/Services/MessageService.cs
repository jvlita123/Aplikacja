using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MessageService
    {
        private readonly MessageRepository _messageRepository;
        public MessageService(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public List<Message> GetAll()
        {
            List<Message> messages = _messageRepository.GetAll().ToList();

            return messages;
        }
    }
}
