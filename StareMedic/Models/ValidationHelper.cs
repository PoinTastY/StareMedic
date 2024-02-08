using System.Text;

namespace StareMedic.Models
{
    public static class ValidationHelper
    {
        public static bool IsNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static string Utf8Validator(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        //soon, CONTPAQi SDK
    }
}
