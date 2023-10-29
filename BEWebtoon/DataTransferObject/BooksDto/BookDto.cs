using BEWebtoon.DataTransferObject.BookFollowsDto;
using BEWebtoon.DataTransferObject.CategoriesBookDto;
using BEWebtoon.DataTransferObject.CommentsDto;

namespace BEWebtoon.DataTransferObject.BooksDto
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public bool? Status { get; set; }
        public List<BookFollowDto>? BookFollows { get; set; }
        public List<CategoryBookDto>? CategoriesBook { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
