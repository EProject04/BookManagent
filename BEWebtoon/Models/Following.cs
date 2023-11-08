using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class Following : EntityAuditBase<int>
    {
        public int? UserId { get; set; }
        public virtual UserProfile? UserProfiles { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
