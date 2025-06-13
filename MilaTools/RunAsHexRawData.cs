using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to convert PE executables to hex raw data and execute raw data.
    /// </summary>
    public class RunAsHexRawData
    {
        public static string ConvertPEToHexRawData(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            if (fileBytes.Length < 2 || fileBytes[0] != 0x4D || fileBytes[1] != 0x5A)
                throw new InvalidDataException("The file is not a valid PE executable.");

            StringBuilder sb = new StringBuilder(fileBytes.Length * 2);
            foreach (byte b in fileBytes)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public static void ExecuteHexRawData(string hexRawData)
        {
            int len = hexRawData.Length;
            if (len % 2 != 0)
                throw new ArgumentException("Hex string length must be even.");

            byte[] bytes = new byte[len / 2];
            for (int i = 0; i < len; i += 2)
                bytes[i / 2] = Convert.ToByte(hexRawData.Substring(i, 2), 16);

            string tempExe = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".exe");
            File.WriteAllBytes(tempExe, bytes);

            Process.Start(tempExe);
        }
    }
}
