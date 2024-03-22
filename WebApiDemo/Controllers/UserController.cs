using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApiDemo.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController: ControllerBase
    {

        private readonly IHubContext<ChatHub> _hubContext;

        public UserController(IHubClients<ChatHub> hubContext)
        {
            _hubContext = (IHubContext<ChatHub>?)hubContext;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] string userName)
        {

            await _hubContext.Clients.All.SendAsync("UserAdded", userName);

            return Ok("User added successfully");
        }

        [HttpGet]
        [Route("HandleUser/{userName}")]
        public async Task<IActionResult> HandleUser(string userName)
        {

            return Ok($"Handling user: {userName}");
        }

    }
}
