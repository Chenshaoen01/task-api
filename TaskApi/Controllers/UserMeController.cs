using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Task.Application.Dtos.User;

using Task.Application.UserHandlers.Queries.GetAllUsers;

namespace TaskApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserMeController : ControllerBase
{
    private readonly GetAllUsersHandler _getAllUsersHandler;

    public UserMeController(GetAllUsersHandler getAllUsersHandler)
    {
        _getAllUsersHandler = getAllUsersHandler;
    }

    // 取得目前登入帳號所屬公司的員工
    [HttpGet("myCompanyMembers")]
    public async Task<ActionResult<IEnumerable<UserGet>>> GetMyCompanyMembers()
    {
        return Ok(await _getAllUsersHandler.Handle());
    }
}