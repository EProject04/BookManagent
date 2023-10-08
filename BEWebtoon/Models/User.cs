using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class User : EntityAuditBase<int>
    { 
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; } =  3 ;
        public virtual UserProfile? UserProfiles { get; set; }
        public virtual Role? Roles { get; set; }
    }
}
