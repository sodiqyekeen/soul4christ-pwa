using System.ComponentModel.DataAnnotations;

namespace YourSoul4Christ.App.Client
{
    public class LoadVerseForm
    {
        [Required]
        public int? Year { get; set; }

        [Required]
        public int? Month { get; set; }
    }
}
