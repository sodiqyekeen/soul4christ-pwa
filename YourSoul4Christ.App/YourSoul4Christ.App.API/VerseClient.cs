using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebPush;
using YourSoul4Christ.App.Entities;
using YourSoul4Christ.App.Models;

namespace YourSoul4Christ.App.API.Services
{
    public class VerseClient : IVerseClient
    {
        private readonly AppDbContext db;

        public VerseClient(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<Verse> AddVerse(CreateVerseViewModel verse)
        {
            if (await db.Verses.AnyAsync(v => v.Book == verse.Book && v.Date.Year == DateTime.Now.Year))
                return null;

            var _verse = new Verse
            {
                Book = verse.Book,
                Content = verse.Content,
                Date = verse.Date.Value
            };
            db.Verses.Add(_verse);
            await db.SaveChangesAsync();

            var subscriptions = await db.NotificationSubscriptions.ToListAsync();
            if (subscriptions.Any())
            {
                _ = SendNotificationAsync(_verse, subscriptions, "Verse for today has been published!");
            }

            return _verse;
        }

        public async Task<Verse> GetVerse(int verseId) =>
            await db.Verses.Where(v => v.Id == verseId).FirstOrDefaultAsync();

        public async Task<IEnumerable<Verse>> GetVerses()
        {
            var verses = await db.Verses.OrderByDescending(x => x.Date).ToListAsync();
            return verses;
        }

        private static async Task SendNotificationAsync(Verse verse, List<NotificationSubscription> subscriptions, string message)
        {
            // For a real application, generate your own
            var publicKey = "BLC8GOevkjdfkhkhdsfClQjyghgf6Cq_a7wJZlstGTVZvwGFFHMYfXt6Njyvgx_GlXJeo5cSiZ1y4JOx1o";
            var privateKey = "OrubzSz3yWACscZXjFQrrtDwCKg-TGFuWhluQ2wLXDo";
            foreach (var subscription in subscriptions)
            {
                var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
                var vapidDetails = new VapidDetails("mailto:<someone@example.com>", publicKey, privateKey);
                var webPushClient = new WebPushClient();
                try
                {
                    var payload = JsonSerializer.Serialize(new
                    {
                        message,
                        url = $"verses/{verse.Id}",
                    });
                    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error sending push notification: " + ex.Message);
                }
            }
        }

        public async Task<Verse> GetVerseForToday() =>
            await db.Verses.Where(v => v.Date.Date == DateTime.Now.Date).FirstOrDefaultAsync();

        public async Task DeleteVerse(Verse verse)
        {
            db.Verses.Remove(verse);
            await db.SaveChangesAsync();
        }
    }
}
