namespace BEWebtoon.DataTransferObject.UsersDto
{
    public class UpdateUserDto : CreateOrUpdateUserDto
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }

    }
}
