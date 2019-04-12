using System.Collections.Generic;

namespace _19GRPADS01BNT401_Assessment.Domain.Entities
{
    public sealed class Book : EntityBase
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }

        public  ICollection<BookAuthor> BookAuthors { get; set; } =new HashSet<BookAuthor>();

    }
}