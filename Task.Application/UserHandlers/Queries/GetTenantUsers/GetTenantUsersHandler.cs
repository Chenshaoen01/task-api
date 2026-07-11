using AutoMapper;
using AutoMapper.QueryableExtensions;
using Task.Application.Dtos.User;
using Task.Application.Interface.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Task.Application.UserHandlers.Queries.GetTenantUsers;

public class GetTenantUsersHandler
{
    private ITaskDbContext _db;
    private IMapper _mapper;
    public GetTenantUsersHandler(ITaskDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserGet>> Handle(GetTenantUsersQuery query)
    {
        return await _db.Users.Where(user => user.TenantId == query.tenantId)
          .ProjectTo<UserGet>(_mapper.ConfigurationProvider)
          .ToListAsync();
    }
}