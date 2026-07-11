using Task.Application.Dtos.User;
namespace Task.Application.UserHandlers.Commands.CreateUser;

public record CreateUserCommand(UserCreate userCreate);