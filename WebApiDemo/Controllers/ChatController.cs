using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly List<ChatInterface> _chatRooms;

        public ChatController(ILogger<ChatController> logger, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("CreateChatRoom")]
        public IActionResult CreateChatRoom([FromBody] ChatInterface model)
        {
            // Создание новой комнаты чата
            var chatRoom = new ChatInterface
            {
                ChatName = model.ChatName, Users = new List<User> { new User { UserName = model.ChatName } }
            };
            _chatRooms.Add(chatRoom);

            return Ok("Chat room created successfully");
        }





    }
}
