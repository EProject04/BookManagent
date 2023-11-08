namespace BEWebtoon.DataTransferObject.UsersDto
{
    public class RegisterUserDto : CreateOrUpdateUserDto
    {
        public int RoleId { get; set; }
    }
}
