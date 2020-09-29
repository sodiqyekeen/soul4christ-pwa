﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using YourSoul4Christ.App.Shared.Entities;

namespace YourSoul4Christ.App.Client
{
    public class AppService
    {
        private readonly HttpClient client;

        public AppService(HttpClient _client)
        {
            client = _client;
        }

        public async Task<IEnumerable<Verse>> GetVerses() =>
            await client.GetFromJsonAsync<Verse[]>("verses");

        public async Task<Verse> GetVerseForToday()
        {
            var response = await client.GetAsync("verses/today");
            if (response.IsSuccessStatusCode)
            {
                var verse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Verse>(verse);
            }
            return null;
        }
    }
}
