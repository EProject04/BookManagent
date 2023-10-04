using BEWebtoon.Models.Domains.Interfaces;
using BEWebtoon.Models.Domains;

namespace BEWebtoon.Models
{
    public class Book : EntityAuditBase<int>, IUserTracking
    {
        public string? BookName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public bool? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public virtual Comment? Commnets { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Following>? Followings { get; set; }
        public virtual ICollection<AuthorBook>? AuthorBooks { get; set; }
        public virtual ICollection<CategoryBook>? CategoryBooks { get; set; }


    }
}
