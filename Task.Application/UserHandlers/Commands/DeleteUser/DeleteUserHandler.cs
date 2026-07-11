using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;
using Task.Domain;

namespace Task.Application.UserHandlers.Commands.DeleteUser;

public class DeleteUserHandler
{
    private ITaskDbContext _db;

    public DeleteUserHandler(ITaskDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        User? targetUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == command.UserId);
        if(targetUser == null || targetUser.TenantId == SystemConstants.SystemTenantId) return false;
        
        _db.Users.Remove(targetUser);
        await _db.SaveChangesAsync();
        return true;
    }
}