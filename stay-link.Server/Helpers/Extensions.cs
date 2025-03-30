using System.Security.Cryptography;
using System.Text;

namespace stay_link.Server.Helpers
{
    public static class Extensions
    {
        public static string ToSHA256(this string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var builder = new StringBuilder();
            builder.Append(char.ToLower(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    builder.Append('_');
                    builder.Append(char.ToLower(input[i]));
                }
                else
                {
                    builder.Append(input[i]);
                }
            }

            return builder.ToString();
        }
    }
}
