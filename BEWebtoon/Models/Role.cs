using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class Role : EntityAuditBase<int>
    {
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
