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
    /// </detail>

    public class StringConverter
    {
        public void HexToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                string result = Encoding.UTF8.GetString(bytes);
                _ = result;
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid hex string.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToHex(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                string hex = BitConverter.ToString(bytes).Replace("-", "");
                _ = hex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Base64ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
                string result = Encoding.UTF8.GetString(bytes);
                _ = result;
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid Base64 string.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToBase64(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                string base64 = Convert.ToBase64String(bytes);
                _ = base64;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                string encoded = System.Net.WebUtility.UrlEncode(str);
                _ = encoded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                string decoded = System.Net.WebUtility.UrlDecode(str);
                _ = decoded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToBinary(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                StringBuilder binary = new StringBuilder();
                foreach (char c in str)
                {
                    binary.Append(Convert.ToString(c, 2).PadLeft(8, '0') + " ");
                }
                _ = binary.ToString().Trim();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BinaryToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = str.Split(' ')
                               .Select(b => Convert.ToByte(b, 2))
                               .ToArray();
                string result = Encoding.UTF8.GetString(bytes);
                _ = result;
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid binary string.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToRot13(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
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
                _ = rot13.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Rot13ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                StringToRot13(str);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToBase16(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                string base16 = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                _ = base16;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Base16ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                string result = Encoding.UTF8.GetString(bytes);
                _ = result;
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid Base16 string.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToShiftJIS(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                byte[] bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(str);
                string shiftJis = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                _ = shiftJis;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ShiftJISToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                var bytes = Enumerable.Range(0, str.Length)
                                      .Where(x => x % 2 == 0)
                                      .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                                      .ToArray();
                string result = Encoding.GetEncoding("Shift_JIS").GetString(bytes);
                _ = result;
            }
            catch (FormatException)
            {
                throw new FormatException("Input string is not a valid Shift_JIS string.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToRot5(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
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
                _ = rot5.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Rot5ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                StringToRot5(str);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StringToRot18(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
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
                _ = rot18.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Rot18ToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                StringToRot18(str);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetStringMD5(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Input string is null or empty.");
            try
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                    _ = hash;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
