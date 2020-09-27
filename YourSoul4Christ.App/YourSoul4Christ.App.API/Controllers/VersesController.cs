using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourSoul4Christ.App.API.Services;
using YourSoul4CHrist.App.Shared.Entities;

namespace YourSoul4Christ.App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersesController : ControllerBase
    {
        private readonly ILogger<VersesController> logger;
        private readonly IVerseClient verseClient;

        public VersesController(ILogger<VersesController> _logger,IVerseClient _verseClient)
        {
            logger = _logger;
            verseClient = _verseClient;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Verse>>> GetVerses()
        {
            var verses = await verseClient.GetVerses(); ;
            return Ok(verses);
        }
    }
}
