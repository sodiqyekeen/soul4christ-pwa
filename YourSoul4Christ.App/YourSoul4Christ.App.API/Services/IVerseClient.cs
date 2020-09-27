using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourSoul4CHrist.App.Shared.Entities;

namespace YourSoul4Christ.App.API.Services
{
    public interface IVerseClient
    {
        public Task<IEnumerable<Verse>> GetVerses();
    }
}
