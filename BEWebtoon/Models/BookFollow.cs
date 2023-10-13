using BEWebtoon.Models.Domains;
using BEWebtoon.Models.Domains.Interfaces;

namespace BEWebtoon.Models
{
    public class BookFollow : EntityAuditBase<int>
    {
        public int? AuthorId { get; set; }
        public int? BookId { get; set; }
        public virtual Author? Authors { get; set; }
        public virtual Book? Books { get; set; }

    }
}
