using Microsoft.EntityFrameworkCore;

namespace TwoDrive.Models;

public class Token {
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }
    public string? Value { get; set; }
    public DateTime? date_created { get; set; }
    public DateTime? date_modified { get; set; }
}