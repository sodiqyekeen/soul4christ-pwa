using System.Collections.Generic;
using System.Threading.Tasks;
using YourSoul4Christ.App.Entities;
using YourSoul4Christ.App.Models;

namespace YourSoul4Christ.App.API.Services
{
    public interface IVerseClient
    {
        public Task<Verse> GetVerse(int verseId);
        public Task<Verse> GetVerseForToday();
        public Task<IEnumerable<Verse>> GetVerses();
        public Task<Verse> AddVerse(CreateVerseViewModel verse);
        public Task DeleteVerse(Verse verse);
    }
}
