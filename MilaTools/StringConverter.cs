using System;
using System.Linq;
using System.Text;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to convert strings between various formats such as Hex, Base64, URL encoding, Binary, and ROT13.
    /// </summary>
    /// 
    /// <detail>
    /// <summary>
    /// Methods List:
    /// </summary>
    /// GetStringLength(string str)
    /// HexToString(string str)
    /// StringToHex(string str)
    /// Base64ToString(string str)
    /// StringToBase64(string str)
    /// Base32ToString(string str)
    /// StringToBase32(string str)
    /// UrlEncode(string str)
    /// UrlDecode(string str)
    /// StringToBinary(string str)
    /// BinaryToString(string str)
    /// StringToRot13(string str)
    /// Rot13ToString(string str)
    /// StringToBase16(string str)
    /// Base16ToString(string str)
    /// StringToShiftJIS(string str)
    /// ShiftJISToString(string str)
    /// StringToRot5(string str)
    /// Rot5ToString(string str)
    /// StringToRot18(string str)
    /// Rot18ToString(string str)
    /// GetStringMD5(string str)
    /// CaesarEncode(string str, int i)
    /// CaesarDecode(string str, int i)
    /// MultiMD5Encrypt(string str, int i)
    /// </detail>

    public class StringConverter
    {
        public static string GetStringLength(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException("Input string is null or empty");
            return str.Length.ToString();
        }

        public static string HexToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid hex string.");
            }
        }

        public static string StringToHex(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            var bytes = Encoding.UTF8.GetBytes(str);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static string Base64ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid Base64 string.");
            }
        }

        public static string StringToBase64(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            return System.Net.WebUtility.UrlEncode(str);
        }

        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            return System.Net.WebUtility.UrlDecode(str);
        }

        // Attention:
        // This method may make the result string very longer than the original string
        // and may not be suitable for all use cases.
        public static string StringToBinary(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            StringBuilder binary = new StringBuilder();
            foreach (char c in str)
            {
                binary.Append(Convert.ToString(c, 2).PadLeft(8, '0') + " ");
            }
            return binary.ToString().Trim();
        }

        public static string BinaryToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = str.Split(' ')
                               .Select(b => Convert.ToByte(b, 2))
                               .ToArray();
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid binary string.");
            }
        }

        public static string StringToRot13(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
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
            return rot13.ToString();
        }

        public static string Rot13ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    // ROT13 is symmetric, so decoding is the same as encoding
                    result.Append((char)((((c - offset + 13) % 26) + offset)));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public static string StringToBase16(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        public static string Base16ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid Base16 string.");
            }
        }

        public static string StringToShiftJIS(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            byte[] bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(str);
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        public static string ShiftJISToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                return Encoding.GetEncoding("Shift_JIS").GetString(bytes);
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid Shift_JIS string.");
            }
        }

        public static string StringToRot5(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            var rot5 = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    rot5.Append((char)((((c - '0' + 5) % 10) + '0')));
                }
                else
                {
                    rot5.Append(c);
                }
            }
            return rot5.ToString();
        }

        public static string Rot5ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    // ROT5 is symmetric for digits
                    result.Append((char)((((c - '0' + 5) % 10) + '0')));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public static string StringToRot18(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
                var rot18 = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    rot18.Append((char)((((c + 13 - offset) % 26) + offset)));
                }
                else if (char.IsDigit(c))
                {
                    rot18.Append((char)((((c - '0' + 5) % 10) + '0')));
                }
                else
                {
                    rot18.Append(c);
                }
            }
            return rot18.ToString();
        }

        public static string Rot18ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    // ROT13 is symmetric, so decoding is the same as encoding
                    result.Append((char)((((c - offset + 13) % 26) + offset)));
                }
                else if (char.IsDigit(c))
                {
                    // ROT5 is also symmetric for digits
                    result.Append((char)((((c - '0' + 5) % 10) + '0')));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        // WARNING:
        // MD5 USING HASH TO ENCRYPT STRINGS SO IT IS NOT REVERSIBLE, ESPECIALLY WHEN USED MULTIPLE TIMES.
        // USE AT YOUR OWN RISK.
        public static string GetStringMD5(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        public static string CaesarEncode(string str, int i)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            var caesar = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    caesar.Append((char)((((c + i - offset) % 26 + 26) % 26 + offset)));
                }
                else
                {
                    caesar.Append(c);
                }
            }
            return caesar.ToString();
        }

        public static string CaesarDecode(string str, int i)
        {
            return CaesarEncode(str, -i);
        }

        // WARNING:
        // MD5 USING HASH TO ENCRYPT STRINGS SO IT IS NOT REVERSIBLE, ESPECIALLY WHEN USED MULTIPLE TIMES.
        // USE AT YOUR OWN RISK.
        public static string MultiMD5Encrypt(string str, int i)
        {
            if (string.IsNullOrEmpty(str) || i < 1)
                throw new ArgumentException("Input string is null or empty or iteration count is less than 1.");
            string result = str;
            for (int j = 0; j < i; j++)
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(result);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    result = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
            return result;
        }
    }
}

// Pray to Mita that this code will work as expected.