using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Task.Domain.Entity;
using Task.Application.Dtos.Tenant;

using Task.Application.TenantHandlers.Queries.GetAllTenants;
using Task.Application.TenantHandlers.Queries.GetTenantById;
using Task.Application.TenantHandlers.Commands.CreateTenant;
using Task.Application.TenantHandlers.Commands.UpdateTenant;
using Task.Application.TenantHandlers.Commands.DeleteTenant;

namespace TaskApi.Controllers;

[ApiController]
[Authorize(Roles = nameof(UserRole.AdminUser))]
[Route("api/[controller]")]
public class TenantController: ControllerBase
{
    private readonly GetAllTenantsHandler _getAllTenantsHandler;
    private readonly GetTenantByIdHandler _getTenantByIdHandler;
    private readonly CreateTenantHandler _createTenantHandler;
    private readonly UpdateTenantHandler _updateTenantHandler;
    private readonly DeleteTenantHandler _deleteTenantHandler;
    public TenantController(
        GetAllTenantsHandler getAllTenantsHandler,
        GetTenantByIdHandler getTenantByIdHandler,
        CreateTenantHandler createTenantHandler,
        UpdateTenantHandler updateTenantHandler,
        DeleteTenantHandler deleteTenantHandler
    )
    {
        _getAllTenantsHandler = getAllTenantsHandler;
        _getTenantByIdHandler = getTenantByIdHandler;
        _createTenantHandler = createTenantHandler;
        _updateTenantHandler = updateTenantHandler;
        _deleteTenantHandler = deleteTenantHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tenant>>> GetAll()
    {
        return Ok(await _getAllTenantsHandler.Handle());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tenant?>> GetById(Guid id)
    {
        Tenant? getResult = await _getTenantByIdHandler.Handle(new GetTenantByIdQuery(id));
        if(getResult == null) return NotFound();
        return Ok(getResult);
    }

    [HttpPost]
    public async Task<ActionResult<Tenant>> Create(TenantCreate TenantCreate)
    {
        return Ok(await _createTenantHandler.Handle(new CreateTenantCommand(TenantCreate)));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Tenant?>> Update(Guid id, TenantUpdate TenantUpdate)
    {
        Tenant? updateResult = await _updateTenantHandler.Handle(new UpdateTenantCommand(id, TenantUpdate));
        if(updateResult == null) return NotFound();
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await _deleteTenantHandler.Handle(new DeleteTenantCommand(id)) ? NoContent() : NotFound();
    }
}