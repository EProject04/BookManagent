using BEWebtoon.DataTransferObject.BookFollowsDto;
using BEWebtoon.DataTransferObject.BooksDto;

namespace BEWebtoon.DataTransferObject.FollowingsDto
{
    public class FollowingDto
    {
        public int? Id { get; set; }
        public IEnumerable<FollowingBookDto>? Books { get; set; }

    }
}
