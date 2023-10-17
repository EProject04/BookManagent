using BEWebtoon.Helpers;

namespace BEWebtoon.DataTransferObject.BooksDto
{
    public class CreateOrUpdateBookDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".svg", ".jpeg" })]
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }
        public bool? Status { get; set; }
        public List<int>? CategoryId { get; set; }
        public List<int>? AuthorId { get; set; }
    }
}
