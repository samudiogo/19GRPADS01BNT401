using System;
using System.Collections.Generic;

namespace _19GRPADS01BNT401_Assessment.UiApi.Models
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class AuthorBooksListModel
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<BookModel> Books { get; set; } = new HashSet<BookModel>();

    }
}