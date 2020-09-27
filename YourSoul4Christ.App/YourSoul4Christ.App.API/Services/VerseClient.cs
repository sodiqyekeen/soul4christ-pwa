using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using YourSoul4CHrist.App.Shared.Entities;

namespace YourSoul4Christ.App.API.Services
{
    public class VerseClient : IVerseClient
    {
        public VerseClient()
        {

        }
        public async Task<IEnumerable<Verse>> GetVerses()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\verses.json");
            var versesText = await File.ReadAllTextAsync(filePath);
            var verses = JsonSerializer.Deserialize<IEnumerable<Verse>>(versesText);
            //var _verses = new Verses(verses);
            return verses;
        }
    }
}
