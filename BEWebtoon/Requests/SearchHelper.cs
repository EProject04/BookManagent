using System.Text.RegularExpressions;

namespace IOCBEWebtoon.Utilities
{
    public static class SearchHelper
    {
        /// <summary>
        /// Convert tiếng việt có dấu sang không dấu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToUnSign(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            //for (int i = 33; i < 48; i++)
            //{
            //    text = text.Replace(((char)i).ToString(), "");
            //}

            //for (int i = 58; i < 65; i++)
            //{
            //    text = text.Replace(((char)i).ToString(), "");
            //}

            //for (int i = 91; i < 97; i++)
            //{
            //    text = text.Replace(((char)i).ToString(), "");
            //}

            //for (int i = 123; i < 127; i++)
            //{
            //    text = text.Replace(((char)i).ToString(), "");
            //}

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string TrimAndLower(this string? str)
        {
            return str == null ? string.Empty : str.Trim().ToLower();
        }

        public static string UpperCaseFirstChar(this string? str)
        {
            return str == null ? string.Empty : Regex.Replace(str, "^[a-z]", m => m.Value.ToUpper());
        }
    }
}
