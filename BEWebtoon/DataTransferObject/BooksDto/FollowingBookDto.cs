namespace BEWebtoon.DataTransferObject.BooksDto
{
    public class FollowingBookDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? Image { get; set; }
        public bool? Status { get; set; }
    }
}
