using BEWebtoon.Models.Domains.Interfaces;

namespace BEWebtoon.Models.Domains
{
    public class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
