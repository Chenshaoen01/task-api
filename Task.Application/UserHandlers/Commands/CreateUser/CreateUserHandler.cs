using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Dtos.User;
using Task.Application.Interface.DataBase;
using Task.Application.Interface.PasswordHasher;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.UserHandlers.Commands.CreateUser;

public class CreateUserHandler
{
    private ITaskDbContext _db;
    private IPasswordHasher _hasher;
    private IMapper _mapper;
    public CreateUserHandler(ITaskDbContext db, IPasswordHasher hasher, IMapper mapper)
    {
        _db = db;
        _hasher = hasher;
        _mapper = mapper;
    }

   public async Task<UserGet?> Handle(CreateUserCommand command)
    {
        User? duplicatedUser = await _db.Users.FirstOrDefaultAsync(user => user.Email == command.userCreate.Email);
        if(duplicatedUser != null) return null;

        User newUser = new User
        {
            Id = Guid.NewGuid(),
            TenantId = command.userCreate.TenantId,
            Name = command.userCreate.Name,
            Email = command.userCreate.Email,
            PasswordHash = _hasher.Hash(command.userCreate.Password)
        };

        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();

        return _mapper.Map<UserGet>(newUser);
    }
}