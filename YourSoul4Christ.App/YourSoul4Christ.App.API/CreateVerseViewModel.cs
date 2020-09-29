using System;
using System.ComponentModel.DataAnnotations;

namespace YourSoul4Christ.App.Models
{
    public class CreateVerseViewModel
    {
        [Required(ErrorMessage = "Passage cannot be empty.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Scripture refrence cannot be empty.")]
        public string Book { get; set; }

        [Required(ErrorMessage ="The date must be specified.")]
        public DateTime? Date { get; set; }
    }
}
