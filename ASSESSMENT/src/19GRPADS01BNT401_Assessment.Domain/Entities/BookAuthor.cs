using System;

namespace _19GRPADS01BNT401_Assessment.Domain.Entities
{
    public class BookAuthor
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}