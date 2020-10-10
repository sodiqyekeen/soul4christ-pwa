using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourSoul4Christ.App.Models;

namespace YourSoul4Christ.App.Client
{
    public class AppService
    {
        private readonly HttpClient client;

        public AppService(HttpClient _client)
        {
            client = _client;
        }

        public async Task<PaginatedList<Verse>> GetPaginatedVerses(PagingOptions pagingOptions)
        {
            IQueryable<Verse> verseQuery = (await client.GetFromJsonAsync<Verse[]>("data/verses.json")).OrderByDescending(v => v.Date).AsQueryable();
            var paginatedList = PaginatedList<Verse>.Create(verseQuery, pagingOptions.Index, pagingOptions.Limit);
            return paginatedList;
        }
        public async Task<PaginatedList<Verse>> GetPaginatedVersesByDate(PagingOptions pagingOptions, int year, int month)
        {
            IQueryable<Verse> verseQuery = (await client.GetFromJsonAsync<Verse[]>("data/verses.json"))
                .Where(v => v.Date.Year == year && v.Date.Month == month)
                .OrderByDescending(v => v.Date)
                .AsQueryable();
            var paginatedList = PaginatedList<Verse>.Create(verseQuery, pagingOptions.Index, pagingOptions.Limit);
            return paginatedList;
        }

        public async Task<IQueryable<Verse>> GetVersesByDateAsQueryable(int year, int month)=>
            (await client.GetFromJsonAsync<Verse[]>("data/verses.json"))
                .Where(v => v.Date.Year == year && v.Date.Month == month)
                .OrderByDescending(v => v.Date)
                .AsQueryable();


        public async Task GetDates()
        {
            var verses = await GetVerses();
            var years = verses.Select(v => v.Date.Year).Distinct();
        }

        public async Task<IEnumerable<Verse>> GetVerses()=>
            (await client.GetFromJsonAsync<Verse[]>("data/verses.json")).OrderByDescending(v => v.Date).ToList();

        public async Task<IEnumerable<Verse>> GetVerseByDate(int year, int month) =>
            (await GetVerses()).Where(v => v.Date.Year == year && v.Date.Month == month).ToList();
        

        public async Task<Verse> GetLatestVerse() =>
            (await GetVerses()).First();

        public async Task<Verse> GetVerseById(string verseId) => 
            (await GetVerses()).Where(v => v.Date.ToString("ddMMyyy") == verseId).FirstOrDefault();

        public async Task<Verse> GetVerseForToday()
        {
            var response = await client.GetAsync("verses/today");
            if (response.IsSuccessStatusCode)
            {
                var verse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Verse>(verse);
            }
            return null;
        }

        //public async Task AddVerse(CreateVerseViewModel verse)
        //{
        //    var verses = (await GetVerses()).ToList();
        //    int maxId = verses.Max(verse => verse.VerseId);
        //    verses.Add(
        //        new Verse
        //        {
        //            VerseId = maxId + 1,
        //            Book = verse.Book,
        //            Content = verse.Content
        //        });
        //    string verseJson = JsonConvert.SerializeObject(this);
        //    await File.WriteAllTextAsync("./wwwwroot/data/verses.json", verseJson);
        //}
    }
}
