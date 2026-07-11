namespace Task.Application.Dtos.User;

public class UserLogin
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}