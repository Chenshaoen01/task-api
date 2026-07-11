using Task.Application.Dtos.User;
namespace Task.Application.Interface.Service;
public interface IAuthService
{
    Task<string?> Login(UserLogin loginInfo);
}