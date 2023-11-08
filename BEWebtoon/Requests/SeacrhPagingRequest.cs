using BEWebtoon.Pagination;

namespace BEWebtoon.Requests
{
    public class SeacrhPagingRequest : PagingRequestBase
    {
        public string? keyword { get; set; }
    }
}
