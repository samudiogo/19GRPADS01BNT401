using System;
using _19GRPADS01BNT401_Assessment.UiApi.Models;

namespace _19GRPADS01BNT401_Assessment.UiApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly User DefaultUser = new User { Username = "samuel", Password = "demo" };



        public User Authenticate(string username, string password)
        {
            if (!DefaultUser.IsValid(username, password))
                return null;
            // authentication successful so return user details without password
            DefaultUser.Password = null;
            return DefaultUser;

        }
    }
}