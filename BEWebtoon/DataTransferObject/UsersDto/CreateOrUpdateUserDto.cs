namespace BEWebtoon.DataTransferObject.UsersDto
{
    public class CreateOrUpdateUserDto
    {
        public string? FisrtName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Address { get; set; }
        public DateTime? DoB { get; set; }
    }
}
