using Infrastructure.Repositories;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MessageService
    {

        private readonly MessageRepository messageRepository;

        public MessageService(MessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task AddMessage(Message message)
        {
            await messageRepository.AddMessage(message);
        }

        public async Task<List<Message>> GetMessagesByChatId(int chatId)
        {
            return await messageRepository.GetMessagesByChatId(chatId);
        }
    }
}
