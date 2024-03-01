using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateChat")]
        public IActionResult CreateChat([FromBody] ChatInterface chat)
        {
            try
            {
                return Ok("Chat created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        
        [HttpGet]
        [Route("GetChat/{chatId}")]
        public IActionResult GetChatById(int Id)
        {
            return Ok("Chat created successfully");
        }
        
        
        /*public IActionResult Get(int id)
        {
            var chat = chats.SingleOrDefault(x => x.Id == id);
            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }*/
        
        /*[HttpGet("StartChat/{Id}", Name = "StartChat")]
        public Chat_Interface Get([FromRoute] int Id)
        {
            
            //return Get().FirstOrDefault(x => x.Id == Id)
        }*/
    }
}