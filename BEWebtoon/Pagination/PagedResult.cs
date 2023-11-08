namespace BEWebtoon.Pagination
{
    public class PagedResult<T> : PagedResultBase
    {
        public IList<T> Items { set; get; } = new List<T>();
        public PagedResult(IList<T> items, int count, int pageNumber, int pageSize)
        {
            TotalRecords = count;
            PageSize = pageSize;
            PageIndex = pageNumber;
            Items = items;
        }
        public static PagedResult<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }
    }
}
