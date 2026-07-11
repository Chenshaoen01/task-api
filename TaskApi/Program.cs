using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskApi.OData;
using Task.Domain.Entity;
using Task.Application;
using Task.Infrastructure;
using Task.Domain;
using Task.Application.Interface.DataBase;
using Task.Application.Interface.PasswordHasher;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddOData(options => options
        .Select()      // 允許 $select
        .Filter()      // 允許 $filter
        .OrderBy()     // 允許 $orderby
        .Count()       // 允許 $count
        .SetMaxTop(100)                                   // $top 上限
        .AddRouteComponents("odata", ODataEDMModelBuilder.Build()));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

const string FrontendCorsPolicy = "FrontendCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

var jwt = builder.Configuration.GetSection("Jwt");
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
        };
    });

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
  {
      var db = scope.ServiceProvider.GetRequiredService<ITaskDbContext>();
      var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

      // 1 確保 System Tenant 存在
      if (!await db.Tenants.AnyAsync(tenant => tenant.Id  == SystemConstants.SystemTenantId))
          db.Tenants.Add(new Tenant { Id = SystemConstants.SystemTenantId, Name = "System" });

      // 2 確保至少一個 SuperUser 存在（用 IgnoreQueryFilters！）
      bool hasAdminUser = await db.Users.IgnoreQueryFilters()
                                    .AnyAsync(user => user.Role == UserRole.AdminUser);
      if (!hasAdminUser)
      {
          db.Users.Add(new User
          {
              Id = Guid.NewGuid(),
              TenantId = SystemConstants.SystemTenantId,
              Role = UserRole.AdminUser,
              Name = "Admin User",
              Email = "adminuser@admin.com",
              PasswordHash = hasher.Hash("123456")
          });
      }
      await db.SaveChangesAsync();
  }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(FrontendCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
