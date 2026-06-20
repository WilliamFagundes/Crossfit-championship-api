using CrossfitChampionship.Application.DTOs.Users;
using CrossfitChampionship.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitChampionship.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _userService.LoginAsync(request);
        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }
}
