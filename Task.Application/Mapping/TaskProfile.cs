using AutoMapper;
using Task.Application.Dtos.Task;
using Task.Application.Dtos.Project;
using Task.Application.Dtos.User;
using Task.Domain.Entity;

namespace Task.Application.Mapping;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskItem, TaskItemGet>()
          .ForMember(dest => dest.AssigneeUserName,
            opt => opt.MapFrom(src => src.AssigneeUser == null ? "" : src.AssigneeUser.Name));
        CreateMap<Project, ProjectGet>();
        CreateMap<User, UserGet>();
    }
}