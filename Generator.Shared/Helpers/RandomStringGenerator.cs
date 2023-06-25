using System.Text;

namespace Generator.Shared.Helpers;

public class RandomStringGenerator
{
    private static Random random = new Random();

    public static string GenerateRandomString()
    {
        char firstChar = (char)random.Next('A', 'Z' + 1);
        return GenerateRandomString(firstChar, 4);
    }

    public static string GenerateRandomString(char firstLetter)
    {
        return GenerateRandomString(firstLetter, 4);
    }

    public static string GenerateRandomString(char firstLetter, int numberOfDigits)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(firstLetter);

        for (int i = 0; i < numberOfDigits; i++)
        {
            int digit = random.Next(0, 10);
            builder.Append(digit);
        }

        return builder.ToString();
    }

}


