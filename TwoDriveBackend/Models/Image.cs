namespace TwoDrive.Models;

public class Image {
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public DateTime? date_created { get; set; }
    public DateTime? date_modified { get; set; }
}