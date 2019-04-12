using System;
using System.ComponentModel.DataAnnotations;

namespace _19GRPADS01BNT401_TP3Web.Models
{
    public class FriendViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}