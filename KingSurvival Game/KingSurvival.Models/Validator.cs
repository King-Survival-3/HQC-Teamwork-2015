namespace KingSurvival.Models
{
    public class Validator
    {
        public static bool IsAllAlphabetic(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }    
            }

            return true;
        }

        public static bool IsString(object value)
        {
            return value is string;
        }

        public static bool IsAlphabeticString(object value)
        {
            string str = value as string;
            return str != null && IsAllAlphabetic(str);
        }
    }
}