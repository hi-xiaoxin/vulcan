using System;
using System.Text;

namespace Dynastech.Patterns
{
    public class PagingQuery<T> : Message, IQuery<PagingQueryResult<T>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public SortDirection SortDirection { get; set; }
        public string SortProperty { get; set; }
    }

    public enum SortDirection
    {
        /// <summary>
        /// 升序，递增
        /// </summary>
        Ascending,

        /// <summary>
        /// 降序，递减
        /// </summary>
        Descending,
    }
}