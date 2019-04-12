using System;
namespace _19GRPADS01BNT401_Assessment.UiApi.Models
{
    public class User
    {
        public Guid Id => Guid.Parse("af88a5c5-f1fc-44b8-9ea7-66ebe7d31512");
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid(string username, string password) => 
            string.Equals(username, Username, StringComparison.Ordinal)
                && string.Equals(password, Password, StringComparison.Ordinal);
    }
}
