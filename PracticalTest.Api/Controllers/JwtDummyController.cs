using PracticalTest.Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PracticalTest.Api.Controllers;

[AllowAnonymous]
public class JwtDummyController : BaseController
{
    private readonly IConfiguration _config;

    public JwtDummyController(ILogger<JwtDummyController> logger, IConfiguration config) : base(logger)
    {
        _config = config;
    }

    [HttpGet]
    public async Task<IActionResult> GetToken()
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, $"{Guid.NewGuid()}")
            };

        return Ok(new
        {
            token = JwtCreationUtil.CreateJwtToken(claims, _config)
        });
    }
}
