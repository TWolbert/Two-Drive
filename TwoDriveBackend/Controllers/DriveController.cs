using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TwoDrive.Models;
using TwoDrive.Models.Requests;
using TwoDrive.Models.Responses;

namespace TwoDrive.Controllers;

[ApiController]
[Route("[controller]")]
public class DriveController : ControllerBase
{
    private readonly TwoDriveContext _context;
    private readonly string key128bit = "12345678901234561234567890123456";

    private readonly ILogger<UserController> _logger;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public DriveController(ILogger<UserController> logger)
    {
        _logger = logger;
        _context = new TwoDriveContext();
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    [HttpGet(Name = "GetDrives")]
    public IEnumerable<Drive> Get()
    {
        return [.. _context.Drive];
    }

    [HttpGet("{id}", Name = "GetDrive")]
    public Drive Get(long id)
    {
        return _context.Drive.Find(id)!;
    }

    // Create drive route
    [HttpPost(Name = "CreateDrive")]
    public Drive Post([FromBody] DriveCreateRequest drive)
    {   
        long userId = 1;

        var newDrive = new Drive
        {
            UserId = userId,
            Name = drive.Name,
            ImageId = drive.ImageId,
            EncryptionKey = drive.EncryptionKey,
            date_created = DateTime.Now,
            date_modified = DateTime.Now
        };

        _context.Drive.Add(newDrive);

        _context.SaveChanges();
        return newDrive;
    }
}
