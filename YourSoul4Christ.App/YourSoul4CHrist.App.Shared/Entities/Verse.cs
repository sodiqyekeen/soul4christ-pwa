using System;
using System.Collections.Generic;
using System.Text;

namespace YourSoul4CHrist.App.Shared.Entities
{
    public class Verse
    {
        public int Id { get; set; }
        public string Book { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
    }
}
