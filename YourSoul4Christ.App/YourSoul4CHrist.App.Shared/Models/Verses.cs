using System;
using System.Collections.Generic;
using System.Text;
using YourSoul4CHrist.App.Shared.Entities;

namespace YourSoul4CHrist.App.Shared.Models
{
    public class Verses:List<Verse>
    {
        public Verses(IEnumerable<Verse> verses)
        {
            this.AddRange(verses);
        }
    }
}
