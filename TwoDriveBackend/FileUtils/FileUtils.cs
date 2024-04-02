namespace TwoDrive.AuthUtils;

using System;

public class FileUtils
{
    public static string SaveDriveIcon(string fileName, byte[] fileData)
    {
        // Check if storage directory exists in local app data
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var specificFolder = Path.Combine(folder, "TwoDriveStorage");

        if (!Directory.Exists(specificFolder))
        {
            Directory.CreateDirectory(specificFolder);
        }

        // Create icons folder if it doesn't exist
        var iconsFolder = Path.Combine(specificFolder, "icons");

        if (!Directory.Exists(iconsFolder))
        {
            Directory.CreateDirectory(iconsFolder);
        }

        // Save file to disk
        var filePath = Path.Combine(iconsFolder, fileName);
        File.WriteAllBytes(filePath, fileData);

        return filePath;
    }
}