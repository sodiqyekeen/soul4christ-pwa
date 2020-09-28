using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YourSoul4Christ.App.Shared.Entities;

namespace YourSoul4Christ.App.API
{
    public class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\verses.json");
            var versesText = File.ReadAllText(filePath);
            var verses = JsonSerializer.Deserialize<IEnumerable<Verse>>(versesText)
                .Select(v => new Verse
                {
                    Book = v.Book,
                    Content = v.Content,
                    Date = v.Date
                }).ToList();

            db.Verses.AddRange(verses);
            db.SaveChanges();
        }
    }
}
