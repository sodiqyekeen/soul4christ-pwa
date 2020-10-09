using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourSoul4Christ.App.Models;

namespace YourSoul4Christ.App.Client
{
    public class VerseList : List<Verse>
    {
        private readonly HttpClient client;
        //private readonly string filePath;

        public VerseList(HttpClient _client)
        {
            client = _client;
            //filePath= "data/verses.json";
            LoadData().GetAwaiter().GetResult();
        }

        //public string FilePath => "data/verses.json";

        private async Task LoadData()
        {
            var _verses = (await client.GetFromJsonAsync<List<Verse>>("data/verses.json")).OrderByDescending(v => v.Date).ToList();
            this.AddRange(_verses);
        }

        public async Task<Verse> GetLatest() =>
            await Task.FromResult(this.First());

        
    }
}
