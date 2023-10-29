using BEWebtoon.Helpers;

namespace BEWebtoon.DataTransferObject.UserProfilesDto
{
    public class CreateOrUpdateUserProfileDto
    {
        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".svg", ".jpeg" })]
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
