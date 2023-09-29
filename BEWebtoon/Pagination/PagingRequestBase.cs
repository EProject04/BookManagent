namespace BEWebtoon.Pagination
{
    public class PagingRequestBase
    {
        private int _pageIndex;
        private int _pageSize;
        public int PageIndex
        {
            get { return _pageIndex == 0 ? 1 : _pageIndex; }
            set { _pageIndex = value; }
        }

        public int PageSize
        {
            get { return _pageSize == 0 ? 10 : _pageSize; }
            set { _pageSize = value; }
        }

        public string? SortField { get; set; }
        public int SortOrder { get; set; }
        public bool? IsPaging { get; set; } = true;
    }
}
