
using BEWebtoon.Models.Domains;
using BEWebtoon.Models.Domains.Interfaces;

namespace BEWebtoon.Models
{
    public class Author : EntityAuditBase<int>
    {
        public string? AuthorName { get; set; }
        public virtual UserProfile? UserProfiles { get; set; }
        public virtual ICollection<AuthorBook>? AuthorBooks { get; set; }
    }
}
