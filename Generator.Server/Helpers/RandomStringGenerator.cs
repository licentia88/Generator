using System.Text;

namespace Generator.Server.Helpers;

public class RandomStringGenerator
{
    private static Random random = new Random();

    public static string GenerateRandomString()
    {
        StringBuilder builder = new StringBuilder();

        // Generate the first character as a random capital letter
        char firstChar = (char)random.Next('A', 'Z' + 1);
        builder.Append(firstChar);

        // Generate the remaining 4 characters as random digits
        for (int i = 0; i < 5; i++)
        {
            int digit = random.Next(0, 10);
            builder.Append(digit);
        }

        return builder.ToString();
    }
}


