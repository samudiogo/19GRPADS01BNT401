using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _19GRPADS01BNT401_Assessment.UiWeb.Models
{
    public class BookViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; }
        [Required]
        [DisplayName("ISBN")]
        public string Isbn { get; set; }
        public int Year { get; set; }

        public IEnumerable<Guid> AuthorsId { get; set; } = new HashSet<Guid>();

    }
    public class BookAuthorsListModel
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        [DisplayName("ISBN")]
        public string Isbn { get; set; }
        public int Year { get; set; }
        public ICollection<AuthorViewModel> Authors { get; set; } = new HashSet<AuthorViewModel>();
    }
}