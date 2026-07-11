using AutoMapper;
using Task.Domain.Entity;
using Task.Application.Dtos.User;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.UserHandlers.Queries.GetUserById;

public class GetUserByIdHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public GetUserByIdHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<UserGet?> Handle(GetUserByIdQuery query)
    {
        User? targetUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == query.userId);
        if(targetUser == null) return null;
        return _mapper.Map<UserGet>(targetUser);
    }
}