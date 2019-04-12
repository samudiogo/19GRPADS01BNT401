using System;
using System.ComponentModel.DataAnnotations;

namespace _19GRPADS01BNT401_TP3Api.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}