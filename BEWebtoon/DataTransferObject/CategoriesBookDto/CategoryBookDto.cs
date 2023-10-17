using BEWebtoon.Models;

namespace BEWebtoon.DataTransferObject.CategoriesBookDto
{
    public class CategoryBookDto
    {
        public int CategoryId { get; set; }
        public int BookId { get; set; }
        public string? CategoryName { get; set; }
    }
}
