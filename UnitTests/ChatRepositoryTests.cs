using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq.EntityFrameworkCore;
using MockQueryable.Moq;
using Infrastructure.Models;

namespace UnitTests
{
    public class ChatRepositoryTests
    {
        private readonly Mock<IApplicationDbContext> mockContext;

        private readonly ChatRepository chatRepository;

        public ChatRepositoryTests()
        {
            mockContext = new Mock<IApplicationDbContext>();
            var chats = new List<Chat>
            {
                new Chat(),
                new Chat()
            };
            mockContext.Setup(x => x.Chats).ReturnsDbSet(chats);
            chatRepository = new ChatRepository(mockContext.Object);
        }

        [Fact]
        public async Task Add_ShouldAddChat()
        {
            var chat = new Chat();
            mockContext.Setup(db => db.Chats.AddAsync(chat, It.IsAny<CancellationToken>()))
                       .Returns(new ValueTask<EntityEntry<Chat>>(Task.FromResult((EntityEntry<Chat>)null)));
            await chatRepository.Add(chat);
            mockContext.Verify(m => m.Chats.AddAsync(chat, It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Delete_ShouldRemoveChatIfExists()
        {
            var chat = new Chat { Id = 1 };
            mockContext.Setup(db => db.Chats.FindAsync(1)).ReturnsAsync(chat);
            await chatRepository.Delete(1);
            mockContext.Verify(m => m.Chats.Remove(chat), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllChats()
        {
            var result = await chatRepository.GetAll();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnChat()
        {
            var chat = new Chat { Id = 1 };
            var mockDbSet = new List<Chat> { chat }.AsQueryable().BuildMockDbSet();
            mockContext.Setup(m => m.Chats).Returns(mockDbSet.Object);
            var result = await chatRepository.GetById(1);
            Assert.Equal(chat, result);
        }

        [Fact]
        public async Task Update_ShouldModifyChat()
        {
            var chat = new Chat();
            mockContext.Setup(db => db.Chats.Update(chat)).Returns(It.IsAny<EntityEntry<Chat>>());
            await chatRepository.Update(chat);
            mockContext.Verify(db => db.Chats.Update(chat), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

    }
}
