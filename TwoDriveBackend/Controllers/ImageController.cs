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
public class ImageController : ControllerBase
{
    private readonly TwoDriveContext _context;

    private readonly ILogger<ImageController> _logger;

    public ImageController(ILogger<ImageController> logger)
    {
        _logger = logger;
        _context = new TwoDriveContext();
    }


    [HttpGet("{id}", Name = "GetImage")]
    public IActionResult Get(long id)
    {
        // return download for image
        var image = _context.Image.Find(id);

        if (image == null)
        {
            return NotFound();
        }

        // check if file exists
        if (!System.IO.File.Exists(image.Path!))
        {
            return NotFound();
        }

        byte[] imageData = System.IO.File.ReadAllBytes(image.Path!);

        return File(imageData, "image/jpeg"); 
    }
}
