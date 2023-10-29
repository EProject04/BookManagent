namespace BEWebtoon.DataTransferObject.CommentsDto
{
    public class CreateOrUpdateCommentDto
    {
        public string? CommentText { get; set; }
        public int? Rate { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
    }
}
