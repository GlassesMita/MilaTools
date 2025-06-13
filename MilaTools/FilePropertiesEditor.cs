using System;
using System.IO;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to add or remove file attributes using FileProperties enum.
    /// </summary>
    public class FilePropertiesEditor
    {
        public void AddProperty(string filePath, FileProperties property)
        {
            FileAttributes attr = File.GetAttributes(filePath);
            attr |= ToFileAttributes(property);
            File.SetAttributes(filePath, attr);
        }

        public void RemoveProperty(string filePath, FileProperties property)
        {
            FileAttributes attr = File.GetAttributes(filePath);
            attr &= ~ToFileAttributes(property);
            File.SetAttributes(filePath, attr);
        }

        private FileAttributes ToFileAttributes(FileProperties property)
        {
            switch (property)
            {
                case FileProperties.Hidden:
                    return FileAttributes.Hidden;
                case FileProperties.System:
                    return FileAttributes.System;
                case FileProperties.Archive:
                    return FileAttributes.Archive;
                case FileProperties.Readonly:
                    return FileAttributes.ReadOnly;
                default:
                    throw new ArgumentException("Unsupported property: " + property);
            }
        }
    }
}
