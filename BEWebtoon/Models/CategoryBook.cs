using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class CategoryBook : EntityAuditBase<int>, IUserTracking
    {
        public int CategoryId { get; set; }
        public int BookId { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public virtual Category? Categories { get; set; }
        public virtual Book? Books { get; set; }
    }
}
