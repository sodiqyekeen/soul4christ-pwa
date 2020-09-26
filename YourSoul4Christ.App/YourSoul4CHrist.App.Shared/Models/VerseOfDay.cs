using System;
using System.Collections.Generic;
using System.Text;

namespace YourSoul4CHrist.App.Shared.Models
{
    public class VerseOfDay
    {
        public int Id { get; set; }
        public string Book { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Title => $"Verse of the Day - {Date:dd/MM/yyyy}";
        public string? Comment { get; set; }
    }
}
