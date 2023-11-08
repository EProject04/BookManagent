namespace BEWebtoon.DataTransferObject.CommentsDto
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public string? CommentText { get; set; }
        public int? Rate { get; set; }
        public string? FullName {  get; set; }
    }
}
