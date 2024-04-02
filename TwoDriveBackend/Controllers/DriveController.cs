using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TwoDrive.AuthUtils;
using TwoDrive.GenericUtils;
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
    public DriveGetResponse Get()
    {
        // Get token from header
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        if (token == null || token == "")
        {
            return new DriveGetResponse {
                drives = [],
                error = "No token provided"
            };
        }

        // Validate token
        if (!ValidateToken.Validate(token))
        {
            return new DriveGetResponse {
                drives = [],
                error = "Invalid token"
            };
        }

        long userId = TokenToUserId.Id(token);

        return new DriveGetResponse {
            drives = [.. _context.Drive.Where(drive => drive.UserId == userId)],
            error = null
        };
    }

    [HttpGet("{id}", Name = "GetDrive")]
    public Drive Get(long id)
    {
        return _context.Drive.Find(id)!;
    }

    // Create drive route
    [HttpPost(Name = "CreateDrive")]
    public CreateDriveResponse Post([FromBody] DriveCreateRequest drive)
    {   
        // Get token from header
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        if (token == null || token == "")
        {
            return new CreateDriveResponse {
                message = null,
                driveUrl = null,
                error = "No token provided"
            };
        }

        // Validate token
        if (!ValidateToken.Validate(token))
        {
            return new CreateDriveResponse {
                message = null,
                driveUrl = null,
                error = "Invalid token"
            };
        }

        long userId = TokenToUserId.Id(token);


        var newImage = new Image
        {
            UserId = userId,
            Path = drive.ImageUrl,
            date_created = DateTime.Now,
            date_modified = DateTime.Now
        };

        _context.Image.Add(newImage);
        _context.SaveChanges();

        var newDrive = new Drive
        {
            UserId = userId,
            Name = drive.Name,
            ImageId = newImage.Id,
            EncryptionKey = BCrypt.Net.BCrypt.HashPassword(drive.EncryptionKey),
            date_created = DateTime.Now,
            date_modified = DateTime.Now
        };

        _context.Drive.Add(newDrive);
        _context.SaveChanges();

        return new CreateDriveResponse {
            message = "Drive created",
            driveUrl = $"{Utils.GetFrontendUrl()}/{newDrive.Id}",
            error = null
        };
    }

    // Upload Drive image route
    [EnableCors("FilePolicy")]
    [HttpPost("icon", Name = "UploadDriveImage")]
    public DriveImageUploadResponse Upload()
    {
        var files = Request.Form.Files;

        if (files.Count == 0)
        {
            return new DriveImageUploadResponse {
                path = null,
                error = "No file provided"
            };
        }

        var image = files[0];

        // Get random name
        string fileName = $"{Path.GetRandomFileName()}_{image.FileName}";

        // Read all data in driveImage.openreadstream
        byte[] fileData = new byte[image.Length];

        using var stream = image.OpenReadStream();
        stream.Read(fileData, 0, (int)image.Length);

        // Save file to disk
        string filePath = FileUtils.SaveDriveIcon(fileName, fileData);

        return new DriveImageUploadResponse {
            path = filePath,
            error = null
        };
    }

    // Drive/Icons get route
    [HttpGet("Icons/{iconName}", Name = "GetDriveIcon")]
    public IActionResult GetIcon(string iconName)
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var specificFolder = Path.Combine(folder, "TwoDriveStorage");
        var iconsFolder = Path.Combine(specificFolder, "icons");

        var filePath = Path.Combine(iconsFolder, iconName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        return PhysicalFile(filePath, "image/png");
    }
}
