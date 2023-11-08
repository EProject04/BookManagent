namespace BEWebtoon.DataTransferObject.AuthorDto
{
    public class CreateAuthorDto : CreateOrUpdateAuthorDto
    {
        public int? RoleId { get; set; }
    }
}
