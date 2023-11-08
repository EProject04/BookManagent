namespace BEWebtoon.DataTransferObject.UserProfilesDto
{
    public class UpdateUserProfileDto : CreateOrUpdateUserProfileDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
    }
}
