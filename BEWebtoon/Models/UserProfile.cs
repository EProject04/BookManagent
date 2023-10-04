using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class UserProfile : EntityAuditBase<int>, IUserTracking
    {

        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Sex { get; set; }
        public int? Role { get; set; }
        public string? ImagePath { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public virtual User? Users { get; set; }
        public virtual Author? Authors { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Following>? Followings { get; set; }
    }
}
