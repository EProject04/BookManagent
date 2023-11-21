using System.Text.RegularExpressions;

namespace BEWebtoon.Helpers
{
    public class ImageHelper
    {
        public static string ImageName(string bookName)
        {
            string pattern = "[/:*?\"<>|]";
            string imageName = Regex.Replace(bookName, pattern, string.Empty);
            imageName = imageName.Replace("\\", string.Empty);
            imageName = imageName.Replace(" ", "-") + ".jpg";
            return imageName;
        }
        public static string UserAvatarName(int userId)
        {
            return userId + ".jpg";
        }
        public static string BookImageUri(string imageName)
        {
            string imageUri = "https://aptechlearningproject.site/uploads/books/" + imageName;
            return imageUri;
        }
        public static string CategoryImageUri(string imageName)
        {
            string imageUri = "https://aptechlearningproject.site/uploads/categories/" + imageName;
            return imageUri;
        }
        public static string UserprofileImageUri(string imageName)
        {
            string imageUri = "https://aptechlearningproject.site/uploads/userprofiles/" + imageName;
            return imageUri;
        }
    }
}
