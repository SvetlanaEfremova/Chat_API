using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MessageRepository
    {

        private readonly IApplicationDbContext dbContext;

        public MessageRepository(IApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task AddMessage(Message message)
        {
            await dbContext.Messages.AddAsync(message);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesByChatId(int chatId)
        {
            return await dbContext.Messages
                .Where(m => m.ChatId == chatId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }
}
