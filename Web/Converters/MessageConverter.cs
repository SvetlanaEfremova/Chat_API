using Infrastructure.Models;
using Web.Dto;
using Web.ViewModels;

namespace Web.Converters
{
    public class MessageConverter
    {
        public static Message ConvertViewModelToMessage(MessageViewModel messageViewModel, int chatId)
        {
            return new Message
            {
                Text = messageViewModel.Text,
                UserId = messageViewModel.UserId,
                ChatId = chatId,
            };
        }

        public static MessageDto ConvertMessageToDto(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                Text = message.Text,
                UserId = message.UserId,
                ChatId = message.ChatId,
            };
        }
    }
}
