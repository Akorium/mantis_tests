using System.Text;
using System;

namespace mantis_tests
{
    public class RandomDataProvider : HelperBase
    {
        public RandomDataProvider(ApplicationManager manager): base(manager) { }
        public static Random random = new Random();
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(random.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                _ = builder.Append(Convert.ToChar(32 + Convert.ToInt32(random.NextDouble() * 65)));
            }
            return builder.ToString();
        }
    }
}
