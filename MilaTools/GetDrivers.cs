using System.Collections.Generic;
using System.IO;
using System.Management;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to get information about all system drives.
    /// </summary>
    public class GetDrivers
    {
        /// <summary>
        /// Gets the number of drives on the system.
        /// </summary>
        public static int GetDriveCount()
        {
            return DriveInfo.GetDrives().Length;
        }

        /// <summary>
        /// Gets detailed information for all drives.
        /// </summary>
        public static List<DriveDetail> GetAllDriveDetails()
        {
            var drives = new List<DriveDetail>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                var detail = new DriveDetail
                {
                    Name = drive.Name,
                    Type = drive.DriveType.ToString(),
                    IsReady = drive.IsReady,
                    FileSystem = drive.IsReady ? drive.DriveFormat : "Unknown",
                    TotalSize = drive.IsReady ? drive.TotalSize : 0,
                    FreeSpace = drive.IsReady ? drive.TotalFreeSpace : 0,
                    UsedSpace = drive.IsReady ? drive.TotalSize - drive.TotalFreeSpace : 0
                };

                // Special handling for CD-ROM drives
                if (drive.DriveType == DriveType.CDRom && !drive.IsReady)
                {
                    detail.FileSystem = "No Media";
                    detail.TotalSize = 0;
                    detail.FreeSpace = 0;
                    detail.UsedSpace = 0;
                }

                drives.Add(detail);
            }
            return drives;
        }

        /// <summary>
        /// Gets the total storage capacity of all drives (bytes).
        /// </summary>
        public static long GetTotalStorage()
        {
            long total = 0;
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    total += drive.TotalSize;
            }
            return total;
        }

        /// <summary>
        /// Gets the total used space of all drives (bytes).
        /// </summary>
        public static long GetTotalUsed()
        {
            long used = 0;
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    used += drive.TotalSize - drive.TotalFreeSpace;
            }
            return used;
        }

        /// <summary>
        /// Gets the total free space of all drives (bytes).
        /// </summary>
        public static long GetTotalFree()
        {
            long free = 0;
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    free += drive.TotalFreeSpace;
            }
            return free;
        }

        /// <summary>
        /// Gets the interface type (e.g., SSD, USB, HDD) of the specified drive letter (e.g., "C").
        /// </summary>
        /// <param name="driveLetter">Drive letter, e.g., "C" or "D".</param>
        /// <returns>Interface type string, or "Unknown" if not found.</returns>
        public static string GetDriveInterfaceType(string driveLetter)
        {
            if (string.IsNullOrWhiteSpace(driveLetter))
                return "Unknown";

            string letter = driveLetter.TrimEnd(':').ToUpper();

            try
            {
                // Find the device ID for the logical disk
                using (var searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '{letter}:'"))
                {
                    foreach (ManagementObject logicalDisk in searcher.Get())
                    {
                        // Get the associated partition
                        using (var partitionSearcher = new ManagementObjectSearcher(
                            $"ASSOCIATORS OF {{Win32_LogicalDisk.DeviceID='{letter}:'}} WHERE AssocClass=Win32_LogicalDiskToPartition"))
                        {
                            foreach (ManagementObject partition in partitionSearcher.Get())
                            {
                                // Get the associated disk drive
                                using (var driveSearcher = new ManagementObjectSearcher(
                                    $"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}} WHERE AssocClass=Win32_DiskDriveToDiskPartition"))
                                {
                                    foreach (ManagementObject disk in driveSearcher.Get())
                                    {
                                        // Try to detect SSD
                                        string mediaType = disk["MediaType"] as string;
                                        string model = disk["Model"] as string;
                                        string interfaceType = disk["InterfaceType"] as string;

                                        // SSD detection (heuristic)
                                        if (!string.IsNullOrEmpty(mediaType) && mediaType.ToLower().Contains("ssd"))
                                            return "SSD";
                                        if (!string.IsNullOrEmpty(model) && model.ToLower().Contains("ssd"))
                                            return "SSD";
                                        if (!string.IsNullOrEmpty(interfaceType))
                                        {
                                            if (interfaceType.ToUpper() == "USB")
                                                return "USB";
                                            if (interfaceType.ToUpper() == "SCSI" || interfaceType.ToUpper() == "IDE" || interfaceType.ToUpper() == "SATA")
                                                return "HDD";
                                        }
                                        return interfaceType ?? "Unknown";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore exceptions and return Unknown
            }
            return "Unknown";
        }
    }

    /// <summary>
    /// Represents detailed information about a drive.
    /// </summary>
    public class DriveDetail
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsReady { get; set; }
        public string FileSystem { get; set; }
        public long TotalSize { get; set; }
        public long UsedSpace { get; set; }
        public long FreeSpace { get; set; }
    }
}
