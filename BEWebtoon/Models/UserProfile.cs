using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class UserProfile : EntityAuditBase<int>
    {

        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? Gender { get; set; }
        public string? ImagePath { get; set; }
        public int? AuthorId { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public virtual User? Users { get; set; }
        public virtual Author? Authors { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual Following? Followings { get; set; }
    }
}
