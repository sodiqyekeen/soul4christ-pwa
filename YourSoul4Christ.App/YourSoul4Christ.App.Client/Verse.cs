using System;

namespace YourSoul4Christ.App.Models
{
    public class Verse
    {
        public int VerseId { get; set; }

        public string Book { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }
    }
}
