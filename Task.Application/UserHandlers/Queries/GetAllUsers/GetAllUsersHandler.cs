using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Task.Application.Dtos.User;
using Task.Application.Interface.DataBase;

namespace Task.Application.UserHandlers.Queries.GetAllUsers;

public class GetAllUsersHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public GetAllUsersHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserGet>> Handle()
    {
        return await _db.Users
          .ProjectTo<UserGet>(_mapper.ConfigurationProvider)
          .ToListAsync();
    }
}