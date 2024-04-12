using System;

namespace WebApiDemo.Services
{
    public interface IChatService
    {
        void Registration(User newUser);
        void SaveUsers();
        int GetUserId();

        int LoginUser(User user);
    }
}
