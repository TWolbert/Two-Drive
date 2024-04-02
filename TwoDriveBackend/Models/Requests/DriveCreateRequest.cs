namespace TwoDrive.Models.Requests;

public class DriveCreateRequest {
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? EncryptionKey { get; set; }
}