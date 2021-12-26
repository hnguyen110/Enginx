using System.Text;
using API.DatabaseContext;
using API.Handlers.Authentication;
using API.Middlewares;
using API.Utilities.CredentialAccessor;
using API.Utilities.JWT.AccessToken;
using API.Utilities.Security;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddFluentValidation(configuration =>
        configuration.RegisterValidatorsFromAssemblyContaining<SignIn.CommandValidator>());
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddMediatR(typeof(SignIn.Handler).Assembly);
builder.Services.AddDbContext<Context>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<ISecurity, Security>();
builder.Services.AddScoped<IAccessToken, AccessToken>();
builder.Services.AddScoped<ICredentialAccessor, CredentialAccessor>();

var server = builder.Build();
server.UseMiddleware<ApiExceptionMiddleware>();
server.UseHttpsRedirection();
server.UseAuthentication();
server.UseAuthorization();
server.MapControllers();
server.Run();