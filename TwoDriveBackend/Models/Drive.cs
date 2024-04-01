namespace TwoDrive.Models;

public class Drive
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public User? User { get; set; }
    public string? Name { get; set; }
    public long? ImageId { get; set; }
    public string? EncryptionKey { get; set; }
    public DateTime? date_created { get; set; }
    public DateTime? date_modified { get; set; }
}