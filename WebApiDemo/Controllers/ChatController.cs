using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace WebApiDemo.Controllers

{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;
        private List<ChatInterface> _chatRooms = new List<ChatInterface>();

        /*private readonly Services.IChatService chatService;
        public ChatController(Services.IChatService services) //конструктор контроллера
        {
            chatService = services;
        }*/
        private int lastUserId;

        public ChatController(ILogger<ChatController> logger, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("AddUserToChat")]
        public async Task<IActionResult> AddUserToChat(string userId)
        {
            await _hubContext.Clients.All.SendAsync("UserAddedToChat", userId);
            return Ok();
        }


        /*[HttpPost]
        [Route("CreateChatRoom")]
        public IActionResult CreateChatRoom([FromBody] ChatInterface model)
        {
            try
            {
                if (model == null)
                {
                    _logger.LogError("Received null model from client.");
                    return BadRequest("Invalid data received from client.");
                }

                // создание новой комнаты чата
                var chatRoom = new ChatInterface
                {
                    ChatName = model.ChatName,
                    UserName = model.ChatName
                };

                _chatRooms.Add(chatRoom);// добавление комнаты в коллекцию чатов

                return Ok(chatRoom);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating chatroom: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }*/





    }
}
