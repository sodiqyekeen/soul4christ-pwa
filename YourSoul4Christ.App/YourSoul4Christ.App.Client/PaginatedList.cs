using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace YourSoul4Christ.App.Client
{
    [DataContract]
    public class PaginatedList<T> : List<T>
    {

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int Offset { get; private set; } // 1 (offset +1) - pageSize (offset + pageSize) of TotalRecords 
        public int TotalRecords { get; set; }
        public bool HasPreviousPage => (CurrentPage > 1);
        public bool HasNextPage => (CurrentPage < TotalPages);
        public int CurrentPageStartIndex => Offset + 1;
        public int CurrentPageLastIndex => Offset + Count;

        public PaginatedList()
        {

        }

        public PaginatedList(List<T> items, int count, int offset, int pageIndex, int pageSize)
        {
            CurrentPage = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalRecords = count;
            Offset = offset;
            this.AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();
            int offset = (pageIndex - 1) * pageSize;
            var items = source.Skip(offset)
                .Take(pageSize)
                .ToList();
            return new PaginatedList<T>(items, count, offset, pageIndex, pageSize);
        }

    }
}
