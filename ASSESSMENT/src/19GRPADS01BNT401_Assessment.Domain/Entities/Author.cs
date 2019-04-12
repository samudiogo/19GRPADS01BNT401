using System;
using System.Collections.Generic;

namespace _19GRPADS01BNT401_Assessment.Domain.Entities
{
    public sealed class Author : EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();

    }
}