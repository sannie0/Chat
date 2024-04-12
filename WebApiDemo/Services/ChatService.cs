using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WebApiDemo.Controllers;

namespace WebApiDemo.Services
{
    public class ChatService: IChatService
    {
        private readonly List<User> users = new List<User>();
        private readonly List<Token> tokens = new List<Token>();
 
        private List<ChatInterface> chats;
        

        private string usersFilePath = "WebApiDemo/WebApiDemo/user.json";
        //private string chatsFilePath = "/Users/svtrev/Downloads/WebApiDemo/WebApiDemo/Json/chats.json";
        // private string messagesFilePath = "/Users/svtrev/Downloads/WebApiDemo/WebApiDemo/Json/messages.json";
        //private string tokensFilePath = "/Users/svtrev/Downloads/WebApiDemo/WebApiDemo/Json/tokens.json";

        public int GetUserId()
        {
            if (users == null) return 0;
            return users.Count();
        }

        public void SaveUsers()
        {
            var Userjson = JsonConvert.SerializeObject(users);
            File.WriteAllText("user.json", Userjson);
        }

        public void Registration(User newUser)
        {
            users.Add(newUser);
            SaveUsers();
        }

        public int LoginUser(User user)
        {
            User RegUser = users.FirstOrDefault(u => u.UserName == user.UserName);
            if (RegUser == null)
            {
                return -1;
            }
            if (RegUser.Password != user.Password)
            {
                return -2;
            }
            string token = Guid.NewGuid().ToString();
            Token newToken = new()
            {
                TokenData = token,
                UserId = RegUser.UserId,
            };

            tokens.Add(newToken);
            SaveUsers();
            return newToken.UserId;
        }
    }
}
