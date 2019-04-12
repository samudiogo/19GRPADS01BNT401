using System;
namespace _19GRPADS01BNT401_TP1.Models
{
    public class FriendViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Selected { get; set; }
    }
}
