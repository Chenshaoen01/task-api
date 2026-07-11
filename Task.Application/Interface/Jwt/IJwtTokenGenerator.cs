using Task.Domain.Entity;

namespace Task.Application.Interface.Jwt;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user, Tenant? tenant);    
}