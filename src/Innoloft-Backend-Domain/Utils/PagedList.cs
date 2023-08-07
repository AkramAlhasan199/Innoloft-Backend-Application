using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Utils
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => PageIndex > 0;

        public bool HasNext => PageIndex < TotalPages - 1;

        public PagedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public dynamic Metadata
        {
            get
            {
                return new
                {
                    TotalCount,
                    PageSize,
                    PageIndex,
                    TotalPages,
                    HasPrevious,
                    HasNext
                };
            }
        }
    }
}
