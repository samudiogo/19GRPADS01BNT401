using System;
using System.Collections.Generic;

namespace _19GRPADS01BNT401_Assessment.UiApi.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
    }

    public class BookCreateEditModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
        public IEnumerable<Guid> AuthorsId { get; set; } = new HashSet<Guid>();
    }

    public class BookAuthorsListModel
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
        public ICollection<AuthorModel> Authors { get; set; } = new HashSet<AuthorModel>();
    }


}