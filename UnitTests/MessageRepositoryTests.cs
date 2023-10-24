using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace UnitTests
{
    public class MessageRepositoryTests
    {
        private readonly Mock<IApplicationDbContext> mockContext;

        private readonly MessageRepository messageRepository;

        public MessageRepositoryTests()
        {
            mockContext = new Mock<IApplicationDbContext>();
            var chats = new List<Chat>
            {
                new Chat(),
                new Chat()
            };
            mockContext.Setup(x => x.Chats).ReturnsDbSet(chats);
            messageRepository = new MessageRepository(mockContext.Object);
        }

        [Fact]
        public async Task AddMessage_ShouldAddMessage()
        {
            var message = new Message();
            mockContext.Setup(db => db.Messages.AddAsync(message, It.IsAny<CancellationToken>()))
                       .Returns(new ValueTask<EntityEntry<Message>>(Task.FromResult((EntityEntry<Message>)null)));
            await messageRepository.AddMessage(message);
            mockContext.Verify(db => db.Messages.AddAsync(message, It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetMessagesByChatId_ShouldReturnMessagesInDescendingOrder()
        {
            var chatId = 1;
            var messages = new List<Message>
            {
                new Message { ChatId = chatId, Date = DateTime.Now.AddDays(-2) },
                new Message { ChatId = chatId, Date = DateTime.Now.AddDays(-1) },
                new Message { ChatId = chatId, Date = DateTime.Now }
            }.AsQueryable();
            mockContext.Setup(x => x.Messages).ReturnsDbSet(messages);
            var result = await messageRepository.GetMessagesByChatId(chatId);
            Assert.Equal(3, result.Count);
            Assert.Equal(DateTime.Now.Date, result[0].Date.Date);
            Assert.Equal(DateTime.Now.AddDays(-1).Date, result[1].Date.Date);
            Assert.Equal(DateTime.Now.AddDays(-2).Date, result[2].Date.Date);
        }
    }
}
