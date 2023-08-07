using Innoloft_Backend_Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Persistence.Extensions
{
    public static class PaginationExtension
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
