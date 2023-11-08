namespace BEWebtoon.DataTransferObject.FollowingsDto
{
    public class UpdateFollowingDto 
    {
        public int Id { get; set; }
        public List<int>? BookIds { get; set; }

    }
}
