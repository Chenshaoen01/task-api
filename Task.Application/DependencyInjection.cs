using Microsoft.Extensions.DependencyInjection;
using Task.Application.Interface.Service;
using Task.Application.Service;

using Task.Application.TenantHandlers.Queries.GetAllTenants;
using Task.Application.TenantHandlers.Queries.GetTenantById;
using Task.Application.TenantHandlers.Commands.CreateTenant;
using Task.Application.TenantHandlers.Commands.UpdateTenant;
using Task.Application.TenantHandlers.Commands.DeleteTenant;

using Task.Application.UserHandlers.Queries.GetAllUsers;
using Task.Application.UserHandlers.Queries.GetTenantUsers;
using Task.Application.UserHandlers.Queries.GetUserById;
using Task.Application.UserHandlers.Commands.CreateUser;
using Task.Application.UserHandlers.Commands.DeleteUser;

using Task.Application.ProjectHandlers.Queries.GetAllProjects;
using Task.Application.ProjectHandlers.Queries.GetProjectById;
using Task.Application.ProjectHandlers.Commands.CreateProject;
using Task.Application.ProjectHandlers.Commands.UpdateProject;
using Task.Application.ProjectHandlers.Commands.DeleteProject;

using Task.Application.TaskHandlers.Queries.GetAllTasks;
using Task.Application.TaskHandlers.Queries.GetAllTasksOData;
using Task.Application.TaskHandlers.Queries.GetTaskById;
using Task.Application.TaskHandlers.Commands.CreateTask;
using Task.Application.TaskHandlers.Commands.UpdateTask;
using Task.Application.TaskHandlers.Commands.DeleteTask;

namespace Task.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<GetAllTenantsHandler>();
        services.AddScoped<GetTenantByIdHandler>();
        services.AddScoped<CreateTenantHandler>();
        services.AddScoped<UpdateTenantHandler>();
        services.AddScoped<DeleteTenantHandler>();

        services.AddScoped<GetAllUsersHandler>();
        services.AddScoped<GetTenantUsersHandler>();
        services.AddScoped<GetUserByIdHandler>();
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<DeleteUserHandler>();

        services.AddScoped<GetAllProjectsHandler>();
        services.AddScoped<GetProjectByIdHandler>();
        services.AddScoped<CreateProjectHandler>();
        services.AddScoped<UpdateProjectHandler>();
        services.AddScoped<DeleteProjectHandler>();

        services.AddScoped<GetAllTasksHandler>();
        services.AddScoped<GetAllTasksODataHandler>();
        services.AddScoped<GetTaskByIdHandler>();
        services.AddScoped<CreateTaskHandler>();
        services.AddScoped<UpdateTaskHandler>();
        services.AddScoped<DeleteTaskHandler>();
        return services;
    }
}