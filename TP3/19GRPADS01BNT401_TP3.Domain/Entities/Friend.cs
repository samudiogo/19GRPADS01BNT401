using System;
namespace _19GRPADS01BNT401_TP3.Domain.Entities
{
    public class Friend
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
