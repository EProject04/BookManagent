using BEWebtoon.Pagination;

namespace BEWebtoon.Requests.BookRequest
{
    public class BookRequest : PagingRequestBase
    {
        public string? keyword { get; set; }
        public string? CategoryName { get; set; }
    }
}
