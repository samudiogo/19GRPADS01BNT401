using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _19GRPADS01BNT401_Assessment.UiWeb.Models
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Birth Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public string FullName => $"{Name} {LastName}";
    }
}