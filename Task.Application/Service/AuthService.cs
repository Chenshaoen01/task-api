using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Task.Application.Interface.Service;
using Task.Application.Dtos.User;
using Task.Application.Interface.PasswordHasher;
using Task.Application.Interface.Jwt;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.Service;
public class AuthService: IAuthService
{
    private ITaskDbContext _db;
    private IPasswordHasher _hasher;
    private IJwtTokenGenerator _jwt;
    public AuthService(ITaskDbContext db, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _hasher = passwordHasher;
        _jwt = jwtTokenGenerator;
    }
    
    public async Task<string?> Login(UserLogin loginInfo)
    {
        User? targetUser = await _db.Users.IgnoreQueryFilters().FirstOrDefaultAsync(user => user.Email == loginInfo.Email); 
        if(targetUser == null) return null;
        bool isValidPassword = _hasher.Verify(loginInfo.Password, targetUser.PasswordHash);
        Tenant? userTenant = await _db.Tenants.FirstOrDefaultAsync(tenant => tenant.Id == targetUser.TenantId);
        if(isValidPassword) return _jwt.GenerateToken(targetUser, userTenant);
        return null;
    }
}