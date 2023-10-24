using Web.ViewModels;
using Infrastructure.Models;
using Web.Dto;

namespace Web.Converters
{
    public class ChatConverter
    {
        public static Chat ConvertViewModelToChat(CreateChatViewModel chatViewModel)
        {
            return new Chat
            {
                Name = chatViewModel.Name,
                CreatedBy = "UnknownUser",
            };
        }

        public static Chat ConvertUpdateViewModelToChat(UpdateChatViewModel chatViewModel)
        {
            return new Chat
            {
                Id = chatViewModel.Id,
                Name = chatViewModel.Name,
                CreatedBy = chatViewModel.CreatedBy,
            };
        }

        public static ChatDto ConvertChatToDto(Chat chat)
        {
            return new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                CreatedBy = chat.CreatedBy,
                Messages = chat.Messages.Select(m => new MessageDto
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    Text = m.Text,
                    Date = m.Date,
                    ChatId = m.ChatId
                }).ToList()
            };
        }
    }
}
