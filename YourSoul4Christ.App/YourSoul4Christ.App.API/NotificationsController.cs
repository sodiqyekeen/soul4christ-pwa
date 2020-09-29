using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourSoul4Christ.App.Entities;

namespace YourSoul4Christ.App.API
{
    [Route("api/notifications")]
    [ApiController]
    [Produces("application/json")]
    public class NotificationsController : ControllerBase
    {
        private readonly AppDbContext db;

        public NotificationsController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpPut("subscribe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
        {
            // We're storing at most one subscription per user, so delete old ones.
            // Alternatively, you could let the user register multiple subscriptions from different browsers/devices.
            //var userId = GetUserId();
            //var oldSubscriptions = db.NotificationSubscriptions.Where(e => e.UserId == userId);
            //db.NotificationSubscriptions.RemoveRange(oldSubscriptions);

            //// Store new subscription
            //subscription.UserId = userId;
            db.NotificationSubscriptions.Attach(subscription);

            await db.SaveChangesAsync();
            return subscription;
        }
    }
}
