namespace TwoDrive.Models.Responses;

public class DriveGetResponse {
    public List<Drive>? drives { get; set; }
    public string? error { get; set; }
}