using System;
using System.IO;
using System.Text;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to identify executable file format and architecture.
    /// </summary>
    public class ExecutableFileIdentification
    {
        public (string Format, string Architecture) Identify(string filePath)
        {
            // Validate file path to prevent path injection
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path must not be null or empty.");

            if (filePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new ArgumentException("File path contains invalid characters.");

            string fullPath = Path.GetFullPath(filePath);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("File does not exist.", fullPath);

            byte[] header = new byte[64];
            using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                fs.Read(header, 0, header.Length);
            }

            // Windows PE/EFI/DOS
            if (header.Length >= 2 && header[0] == 0x4D && header[1] == 0x5A) // "MZ"
            {
                // DOS stub, check for PE header
                int peOffset = BitConverter.ToInt32(header, 0x3C);
                byte[] peHeader = new byte[6];
                using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    fs.Seek(peOffset, SeekOrigin.Begin);
                    fs.Read(peHeader, 0, peHeader.Length);
                }
                if (peHeader[0] == 0x50 && peHeader[1] == 0x45 && peHeader[2] == 0x00 && peHeader[3] == 0x00) // "PE\0\0"
                {
                    ushort machine = BitConverter.ToUInt16(peHeader, 4);
                    string arch = GetPEArchitecture(machine);
                    return ("Windows or EFI Program", arch);
                }
                else
                {
                    return ("Windows or DOS Program", "x86 (assumed)");
                }
            }

            // Linux ELF
            if (header.Length >= 4 && header[0] == 0x7F && header[1] == (byte)'E' && header[2] == (byte)'L' && header[3] == (byte)'F')
            {
                string arch = GetELFArchitecture(header);
                return ("Linux Executable", arch);
            }

            // macOS Mach-O (Fat or Thin)
            uint magic = BitConverter.ToUInt32(header, 0);
            if (magic == 0xFEEDFACE || magic == 0xCEFAEDFE || magic == 0xFEEDFACF || magic == 0xCFFAEDFE ||
                magic == 0xCAFEBABE || magic == 0xBEBAFECA)
            {
                string arch = GetMachOArchitecture(header);
                return ("macOS Executable", arch);
            }

            return ("Unknown", "Unknown");
        }

        private string GetPEArchitecture(ushort machine)
        {
            switch (machine)
            {
                case 0x014c: return "x86";
                case 0x8664: return "x86_64";
                case 0x01c0: return "ARM";
                case 0xAA64: return "ARM64";
                default: return "Unknown";
            }
        }

        private string GetELFArchitecture(byte[] header)
        {
            if (header.Length < 20) return "Unknown";
            string bits = header[4] == 1 ? "32" : header[4] == 2 ? "64" : "?";
            ushort machine = BitConverter.ToUInt16(header, 18);
            switch (machine)
            {
                case 0x03: return "x86-" + bits;
                case 0x3E: return "x86_64";
                case 0x28: return "ARM";
                case 0xB7: return "ARM64";
                default: return "Unknown";
            }
        }

        private string GetMachOArchitecture(byte[] header)
        {
            uint magic = BitConverter.ToUInt32(header, 0);
            bool is64 = (magic == 0xFEEDFACF || magic == 0xCFFAEDFE);
            int cputypeOffset = 4;
            int cputype = BitConverter.ToInt32(header, cputypeOffset);
            switch (cputype)
            {
                case 7: return is64 ? "x86_64" : "x86";
                case 12: return is64 ? "ARM64" : "ARM";
                default: return "Unknown";
            }
        }
    }
}
