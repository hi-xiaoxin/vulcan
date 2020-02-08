using System.Collections.Generic;

namespace Dynastech.Patterns
{
    public class PagingQueryResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }

}
