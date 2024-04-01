namespace TwoDrive.Models.Requests;

public class DriveCreateRequest {
    public string? Name { get; set; }
    public long? ImageId { get; set; }
    public string? EncryptionKey { get; set; }
}