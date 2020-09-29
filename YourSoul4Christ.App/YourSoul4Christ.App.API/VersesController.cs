using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourSoul4Christ.App.API.Services;
using YourSoul4Christ.App.Entities;
using YourSoul4Christ.App.Models;

namespace YourSoul4Christ.App.API.Controllers
{
    [ResponseCache(Duration = 60, VaryByQueryKeys = new string[] { "*" })]
    [Route("api/verses")]
    [ApiController]
    [Produces("application/json")]
    public class VersesController : ControllerBase
    {
        private readonly ILogger<VersesController> logger;
        private readonly IVerseClient verseClient;

        public VersesController(ILogger<VersesController> _logger, IVerseClient _verseClient)
        {
            logger = _logger;
            verseClient = _verseClient;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Verse>>> GetVerses()
        {
            var verses = await verseClient.GetVerses(); ;
            return Ok(verses);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Verse>> PostVerse(CreateVerseViewModel verse)
        {
            var _newVerse = await verseClient.AddVerse(verse);
            if (_newVerse == null)
                return BadRequest(new { Error = "Verse already exist." });
            return StatusCode(StatusCodes.Status201Created, _newVerse);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Verse>> GetVerse(int id)
        {
            var verse = await verseClient.GetVerse(id);
            return verse == null ? NotFound() : (ActionResult<Verse>)Ok(verse);
        }

        [HttpGet("today")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Verse>> GetVerseForToday()
        {
            var verse = await verseClient.GetVerseForToday();
            return verse == null ? NotFound() : (ActionResult<Verse>)Ok(verse);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVerse(int verseId)
        {
            var verse = await verseClient.GetVerse(verseId);
            if (verse == null)
                return NotFound();
            await verseClient.DeleteVerse(verse);
            return NoContent();
        }
    }
}
