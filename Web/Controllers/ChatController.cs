using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Infrastructure.Models;
using BusinessLogic.Services;
using Web.Converters;

namespace Web.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService chatService;

        private readonly MessageService messageService;

        private readonly IHubContext<ChatHub> hubContext;

        public ChatController(ChatService chatService, MessageService messageService, IHubContext<ChatHub> hubContext)
        {
            this.chatService = chatService;
            this.messageService = messageService;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatViewModel chatViewModel, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId is required");
            if (chatViewModel == null)
                return BadRequest("Chat object is null");
            var chat = ChatConverter.ConvertViewModelToChat(chatViewModel);
            chat.CreatedBy = userId;
            await chatService.Add(chat);
            return CreatedAtRoute("GetChatById", new { id = chat.Id }, chat);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChats()
        {
            var chats = await chatService.GetAll();
            return Ok(chats);
        }

        [HttpGet("{id}", Name = "GetChatById")]
        public async Task<IActionResult> GetChatById(int id)
        {
            var chat = await chatService.GetById(id);
            if (chat == null)
            {
                return NotFound("Chat with given id not found");
            }
            return Ok(ChatConverter.ConvertChatToDto(chat)); ;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChat(int id, [FromBody] UpdateChatViewModel chatViewModel, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId is required");
            if (chatViewModel == null || id != chatViewModel.Id)
                return BadRequest("Chat object or ID is incorrect");
            var existingChat = await chatService.GetById(id);
            if (existingChat == null)
                return NotFound("Chat with given id not found");
            var chat = ChatConverter.ConvertUpdateViewModelToChat(chatViewModel);
            await chatService.Update(chat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId is required");
            var chat = await chatService.GetById(id);
            if (chat == null)
                return NotFound("Chat with given id not found");
            if (chat.CreatedBy != userId)
                return BadRequest("User is not authorized to delete this chat");
            await chatService.Delete(id);
            await hubContext.Clients.All.SendAsync("ChatDeleted", id);
            return NoContent();
        }

        [HttpPost("{chatId}/messages")]
        public async Task<IActionResult> SendMessage(int chatId, [FromBody] MessageViewModel messageViewModel)
        {
            var chat = chatService.GetById(chatId);
            if (chat == null)
                return NotFound("Chat with given id not found");
            var message = MessageConverter.ConvertViewModelToMessage(messageViewModel, chatId);
            await messageService.AddMessage(message);
            var messageDto = MessageConverter.ConvertMessageToDto(message);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", messageViewModel.UserId.ToString(), messageViewModel.Text);
            return Ok(messageDto);
        }

        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var chat = await chatService.GetById(chatId);
            if (chat == null)
                return NotFound("Chat with given id not found");
            var messages = await messageService.GetMessagesByChatId(chatId);
            return Ok(messages);
        }
    }
}
