using System;
using System.Linq;
using System.Text;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to convert strings between various formats such as Hex, Base64, URL encoding, Binary, and ROT13.
    /// </summary>
    public class StringConverter
    {
        public void HexToString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                string result = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a valid hex string.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void StringToHex(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                string hex = BitConverter.ToString(bytes).Replace("-", "");
                Console.WriteLine(hex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Base64ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
                string result = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a valid Base64 string.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void StringToBase64(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                string base64 = Convert.ToBase64String(bytes);
                Console.WriteLine(base64);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                string encoded = System.Net.WebUtility.UrlEncode(str);
                Console.WriteLine(encoded);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                string decoded = System.Net.WebUtility.UrlDecode(str);
                Console.WriteLine(decoded);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void StringToBinary(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                StringBuilder binary = new StringBuilder();
                foreach (char c in str)
                {
                    binary.Append(Convert.ToString(c, 2).PadLeft(8, '0') + " ");
                }
                Console.WriteLine(binary.ToString().Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void BinaryToString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                var bytes = str.Split(' ')
                               .Select(b => Convert.ToByte(b, 2))
                               .ToArray();
                string result = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a valid binary string.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void StringToRot13(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                var rot13 = new StringBuilder();
                foreach (char c in str)
                {
                    if (char.IsLetter(c))
                    {
                        char offset = char.IsUpper(c) ? 'A' : 'a';
                        rot13.Append((char)((((c + 13 - offset) % 26) + offset)));
                    }
                    else
                    {
                        rot13.Append(c);
                    }
                }
                Console.WriteLine(rot13.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Rot13ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Input string is null or empty.");
                return;
            }
            try
            {
                StringToRot13(str); // ROT13 is symmetric, so we can use the same method
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
