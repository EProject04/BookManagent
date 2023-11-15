using System.Text.RegularExpressions;

namespace BEWebtoon.Helpers
{
    public class ImageHelper
    {
        public static string ImageName(string bookName)
        {
            string pattern = "[/:*?\"<>|]";
            string imagePath = Regex.Replace(bookName, pattern, string.Empty);
            imagePath = imagePath.Replace("\\", string.Empty);
            imagePath = imagePath.Replace(" ", "-") + ".jpg";
            return imagePath;
        }
        public static string BookImageUri(string imageName)
        {
            string imageUri = "https://aptechlearningproject.site/book/" + imageName;
            return imageUri;
        }
        public static string CategoryImageUri(string imageName)
        {
            string imageUri = "https://aptechlearningproject.site/category/" + imageName;
            return imageUri;
        }
    }
}
