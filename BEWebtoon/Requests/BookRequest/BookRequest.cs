using BEWebtoon.Pagination;

namespace BEWebtoon.Requests.BookRequest
{
    public class BookRequest : PagingRequestBase
    {
        public string? keyword { get; set; }
        public string? CategoryName { get; set; }
        public int? CategoryID { get; set; }
        public string? AuthorName { get; set; }
        public int? AuthorID { get; set; }
    }
}
