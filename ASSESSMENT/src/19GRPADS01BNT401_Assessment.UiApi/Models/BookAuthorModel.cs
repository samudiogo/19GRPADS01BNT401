using System;
namespace _19GRPADS01BNT401_Assessment.UiApi.Models
{
    public class BookAuthorModel
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }

        public string BookTitle { get; set; }
        public string BookIsbn { get; set; }
        public int BookYear { get; set; }

        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime AuthorBirthDate { get; set; }

    }

    public class BookAuthorAddEditModel
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
