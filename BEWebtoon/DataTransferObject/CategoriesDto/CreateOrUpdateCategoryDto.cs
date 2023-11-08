using BEWebtoon.Helpers;

namespace BEWebtoon.DataTransferObject.CategoriesDto
{
    public class CreateOrUpdateCategoryDto
    {

        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".svg", ".jpeg" })]
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }
    }
}
