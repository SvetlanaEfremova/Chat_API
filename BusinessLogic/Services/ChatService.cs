using Infrastructure.Models;
using Infrastructure.Repositories;

namespace BusinessLogic.Services
{
    public class ChatService
    {
        private readonly ChatRepository chatRepository;

        public ChatService(ChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task Add(Chat chat)
        {
            await chatRepository.Add(chat);
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            return await chatRepository.GetAll();
        }

        public async Task<Chat> GetById(int id)
        {
            return await chatRepository.GetById(id);
        }

        public async Task Update(Chat chat)
        {
            await chatRepository.Update(chat);
        }

        public async Task Delete(int id)
        {
            await chatRepository.Delete(id);
        }
    }

}