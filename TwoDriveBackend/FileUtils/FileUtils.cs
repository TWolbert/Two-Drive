namespace TwoDrive.AuthUtils;

using System;
using System.Security.Cryptography;
using System.Text;

public class FileUtils
{
    /// <summary>
    /// This function saves a file, unencrypted. Please use the save() function for encrypted files.
    /// </summary>
    /// <param name="fileName">Name of the file you want to save</param>
    /// <param name="fileData">Byte array of data</param>
    /// <returns>String with file path</returns>
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

    public static byte[] Encrypt(string encryptionKey, byte[] fileData) {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
        aes.IV = new byte[16];

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        cs.Write(fileData, 0, fileData.Length);
        cs.Close();

        return ms.ToArray();
    }
}