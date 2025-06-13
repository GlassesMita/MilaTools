using System;
using Microsoft.Win32;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to add, modify, query, and delete registry values.
    /// </summary>
    public class RegisterAction
    {
        /// <summary>
        /// Adds or modifies a registry value.
        /// </summary>
        /// <param name="root">Registry root (e.g., Registry.CurrentUser).</param>
        /// <param name="subKey">Subkey path.</param>
        /// <param name="name">Value name.</param>
        /// <param name="value">Value data.</param>
        /// <param name="type">Value type (see RegistryValueType enum).</param>
        public void SetValue(RegistryKey root, string subKey, string name, object value, RegistryValueType type)
        {
            using (var key = root.CreateSubKey(subKey, true))
            {
                if (key == null)
                    throw new InvalidOperationException("Failed to open or create registry key.");
                key.SetValue(name, value, ToRegistryValueKind(type));
            }
        }

        /// <summary>
        /// Queries a registry value.
        /// </summary>
        /// <param name="root">Registry root.</param>
        /// <param name="subKey">Subkey path.</param>
        /// <param name="name">Value name.</param>
        /// <returns>Value data, or null if not found.</returns>
        public object GetValue(RegistryKey root, string subKey, string name)
        {
            using (var key = root.OpenSubKey(subKey, false))
            {
                if (key == null)
                    return null;
                return key.GetValue(name, null);
            }
        }

        /// <summary>
        /// Deletes a registry value.
        /// </summary>
        /// <param name="root">Registry root.</param>
        /// <param name="subKey">Subkey path.</param>
        /// <param name="name">Value name.</param>
        public void DeleteValue(RegistryKey root, string subKey, string name)
        {
            using (var key = root.OpenSubKey(subKey, true))
            {
                if (key != null)
                {
                    key.DeleteValue(name, false);
                }
            }
        }

        /// <summary>
        /// Deletes a registry key.
        /// </summary>
        /// <param name="root">Registry root.</param>
        /// <param name="subKey">Subkey path.</param>
        public void DeleteKey(RegistryKey root, string subKey)
        {
            root.DeleteSubKeyTree(subKey, false);
        }

        private RegistryValueKind ToRegistryValueKind(RegistryValueType type)
        {
            switch (type)
            {
                case RegistryValueType.String:
                    return RegistryValueKind.String;
                case RegistryValueType.ExpandString:
                    return RegistryValueKind.ExpandString;
                case RegistryValueType.DWord:
                    return RegistryValueKind.DWord;
                case RegistryValueType.QWord:
                    return RegistryValueKind.QWord;
                case RegistryValueType.Binary:
                    return RegistryValueKind.Binary;
                case RegistryValueType.MultiString:
                    return RegistryValueKind.MultiString;
                default:
                    throw new ArgumentException("Unsupported registry value type.");
            }
        }
    }
}
