using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class CategoryBook : EntityAuditBase<int>
    {
        public int CategoryId { get; set; }
        public int BookId { get; set; }
        public virtual Category? Categories { get; set; }
        public virtual Book? Books { get; set; }
    }
}
