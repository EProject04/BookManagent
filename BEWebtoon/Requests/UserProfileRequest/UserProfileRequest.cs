using BEWebtoon.Pagination;

namespace BEWebtoon.Requests.UserProfileRequest
{
    public class UserProfileRequest : PagingRequestBase
    {
        public string? keyword { get; set; }
        public int? AuthorId { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
