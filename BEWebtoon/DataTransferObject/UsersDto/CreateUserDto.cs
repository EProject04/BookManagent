namespace BEWebtoon.DataTransferObject.UsersDto
{
    public class CreateUserDto : CreateOrUpdateUserDto
    {
        public int? RoleId { get; set; }
    }
}
