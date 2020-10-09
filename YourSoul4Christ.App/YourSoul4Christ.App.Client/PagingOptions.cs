using System;
using System.ComponentModel.DataAnnotations;

namespace YourSoul4Christ.App.Client
{
    public class PagingOptions
    {
        [Range(1, 99999, ErrorMessage = "Index must be greater than 0.")]
        public int Index { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int Limit { get; set; } = 7;
    }
}
