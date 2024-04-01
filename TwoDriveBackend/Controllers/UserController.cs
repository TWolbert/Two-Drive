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
public class UserController : ControllerBase
{
    private readonly TwoDriveContext _context;
    private readonly string key128bit = "12345678901234561234567890123456";

    private readonly ILogger<UserController> _logger;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
        _context = new TwoDriveContext();
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> Get()
    {
        return [.. _context.User];
    }

    [HttpGet("{id}", Name = "GetUser")]
    public User Get(long id)
    {
        return _context.User.Find(id)!;
    }

    // Create user route
    [HttpPost(Name = "CreateUser")]
    public User Post([FromBody] UserCreateRequest user)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var newUser = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = passwordHash,
            date_created = DateTime.Now,
            date_modified = DateTime.Now
        };

        _context.User.Add(newUser);

        _context.SaveChanges();
        return newUser;
    }

    // Login function
    [HttpPost("login", Name = "Login")]
    public AuthLoginResponse Login([FromBody] UserLoginRequest user)
    {
        var userFound = _context.User.FirstOrDefault(u => u.Name == user.Name);
        if (userFound == null)
        {
            return new AuthLoginResponse
            {
                error = "User not found"
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(user.Password, userFound.Password))
        {
            return new AuthLoginResponse
            {
                error = "Invalid password"
            };
        }

        var token = _tokenHandler.WriteToken(new JwtSecurityToken(
            expires: DateTime.Now.AddHours(3),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key128bit)), SecurityAlgorithms.HmacSha256)
        ));

        _context.Token.Add(new Token
        {
            UserId = userFound.Id,
            Value = token,
            date_created = DateTime.Now,
            date_modified = DateTime.Now
        });

        _context.SaveChanges();

        return new AuthLoginResponse
        {
            token = token,
            message = "Login successful"
        };
    }

    // Token validation function
    [HttpPost("validate", Name = "ValidateToken")]
    public AuthValidateResponse ValidateToken([FromBody] AuthValidateRequest token)
    {
        try
        {
            _tokenHandler.ValidateToken(token.Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key128bit)),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out _);

            return new AuthValidateResponse
            {
                message = "Token is valid"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new AuthValidateResponse
            {
                error = "Token is invalid"
            };
        }
    }
}
