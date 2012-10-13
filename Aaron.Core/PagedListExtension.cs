using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using Aaron.Core;

namespace System.Linq
{
    public static class PagedListExtension
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
            //return null;
        }

        public static IPagedList<T> ToPagedList<T>(this ICollection<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
            //return null;
        }

        public static IPagedList<T> ToPagedList<T>(this IList<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
            //return null;
        }
    }
}
