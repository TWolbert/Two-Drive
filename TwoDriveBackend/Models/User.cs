namespace TwoDrive.Models;

public class User {
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime? date_created { get; set; }
    public DateTime? date_modified { get; set; }
    public List<Token>? Tokens { get; set; }
}