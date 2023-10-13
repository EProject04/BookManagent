using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class Book : EntityAuditBase<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public bool? Status { get; set; }
        public virtual Comment? Commnets { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Following>? Followings { get; set; }
        public virtual ICollection<BookFollow>? BookFollows { get; set; }
        public virtual ICollection<CategoryBook>? CategoryBooks { get; set; }


    }
}
