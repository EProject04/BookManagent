using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class Category : EntityAuditBase<int>
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public virtual ICollection<CategoryBook>? CategoryBooks { get; set; }
    }
}
