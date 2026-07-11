using Microsoft.AspNetCore.Mvc;
using Task.Application.Dtos.User;
using Task.Application.Interface.Service;

namespace TaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin loginInfo)
    {
        string? result = await _service.Login(loginInfo);
        if(result != null) return Ok(result);
        return BadRequest("帳號或密碼錯誤");
    }
}
