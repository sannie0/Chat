using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;
using FluentAssertions.Common;
using WebApiDemo.Services;

namespace WebApiDemo.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController: ControllerBase
    {
        //private readonly IHubContext<ChatHub> _hubContext;
        
        /*public UserController(IHubClients<ChatHub> hubContext)
        {
            _hubContext = (IHubContext<ChatHub>?)hubContext;
        }*/
        private readonly ILogger<UserController> _logger;
        private readonly IChatService _chatService;
        
        private readonly Services.IChatService chatService;
        public UserController(ILogger<UserController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));// преобразование пароля в байтовый массив

                StringBuilder builder = new StringBuilder();// преобразование байтового массива в строку HEX
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUserAsync(string username, string password)
        {
            try
            {
                User newUser = new()
                {
                    UserId = 0,
                    UserName = username,
                    Password = HashPassword(password)
                };
                int token = _chatService.LoginUser(newUser);
                if (token == -1)
                {
                    return BadRequest("User not found!");
                }
                if (token == -2)
                {
                    return BadRequest("Password is wrong");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while registration user: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(string username, string password)
        {
            try
            {
                if (username == null || password == null)
                {
                    return BadRequest("Invalid data received from client.");
                }
                var newUser = new User
                {
                    UserId = _chatService.GetUserId(),
                    UserName = username,
                    Password = HashPassword(password)
                };

                _chatService.Registration(newUser);
                _logger.LogInformation("Данные получены!");
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while registration user: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
