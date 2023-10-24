using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChatRepository
    {
        private readonly IApplicationDbContext dbContext;

        public ChatRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(Chat chat)
        {
            await dbContext.Chats.AddAsync(chat);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var chat = await dbContext.Chats.FindAsync(id);
            if (chat != null)
            {
                dbContext.Chats.Remove(chat);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            return await dbContext.Chats.Include(c => c.Messages).ToListAsync(); 
        }

        public async Task<Chat> GetById(int id)
        {
            return await dbContext.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Chat chat)
        {
            dbContext.Chats.Update(chat);
            await dbContext.SaveChangesAsync();
        }
    }
}
