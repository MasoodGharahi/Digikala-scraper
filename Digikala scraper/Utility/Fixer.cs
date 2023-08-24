namespace Digikala_scraper.Utility
{
    public static class Fixer
    {
        public static double ToEnglishDouble(string input)
        {
      
                string[] persianDigits = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
                for (int j = 0; j < persianDigits.Length; j++)
                    input = input.Replace(persianDigits[j], j.ToString());
                var result = Convert.ToDouble(input);
                return result;
            
        }
    }
}
