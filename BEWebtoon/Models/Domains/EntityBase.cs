using BEWebtoon.Models.Domains.Interfaces;

namespace BEWebtoon.Models.Domains
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
