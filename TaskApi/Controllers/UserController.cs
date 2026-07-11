using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Task.Application.Dtos.User;
using Task.Domain.Entity;

using Task.Application.UserHandlers.Queries.GetAllUsers;
using Task.Application.UserHandlers.Queries.GetTenantUsers;
using Task.Application.UserHandlers.Queries.GetUserById;
using Task.Application.UserHandlers.Commands.CreateUser;
using Task.Application.UserHandlers.Commands.DeleteUser;

namespace TaskApi.Controllers;

[ApiController]
[Authorize(Roles = nameof(UserRole.AdminUser))]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsersHandler _getAllUsersHandler;
    private readonly GetTenantUsersHandler _getTenantUsersHandler;
    private readonly GetUserByIdHandler _getUserByIdHandler;
    private readonly CreateUserHandler _createUserHandler;
    private readonly DeleteUserHandler _deleteUserHandler;

    public UserController(
        GetAllUsersHandler getAllUsersHandler,
        GetTenantUsersHandler getTenantUsersHandler,
        GetUserByIdHandler getUserByIdHandler,
        CreateUserHandler createUserHandler,
        DeleteUserHandler deleteUserHandler
    )
    {
        _getAllUsersHandler = getAllUsersHandler;
        _getTenantUsersHandler = getTenantUsersHandler;
        _getUserByIdHandler = getUserByIdHandler;
        _createUserHandler = createUserHandler;
        _deleteUserHandler = deleteUserHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserGet>>> GetAll()
    {
        return Ok(await _getAllUsersHandler.Handle());
    }

    [HttpGet("GetTenantUser/{id}")]
    public async Task<ActionResult<IEnumerable<UserGet>>> GetTenantUsers(Guid id)
    {
        return Ok(await _getTenantUsersHandler.Handle(new GetTenantUsersQuery(id)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserGet?>> GetById(Guid id)
    {
        UserGet? user = await _getUserByIdHandler.Handle(new GetUserByIdQuery(id));
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserGet>> Create(UserCreate item)
    {
        UserGet? created = await _createUserHandler.Handle(new CreateUserCommand(item));
        if(created == null) return BadRequest("此 Email 已被註冊");
        return CreatedAtAction(nameof(GetById), new { id = created.Id}, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _deleteUserHandler.Handle(new DeleteUserCommand(id)) ? NoContent() : NotFound();
    }
}
