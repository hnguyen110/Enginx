using System.Text;
using API.DatabaseContext;
using API.Handlers.Authentication;
using API.Middlewares;
using API.Repositories.Account;
using API.Repositories.Address;
using API.Repositories.ContactInformation;
using API.Repositories.Profile;
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
        opt.UseSqlite(builder.Configuration.GetConnectionString("Database"))
// opt.UseSqlServer(builder.Configuration.GetConnectionString("SQL_SERVER"))
);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<ISecurity, Security>();
builder.Services.AddScoped<IAccessToken, AccessToken>();
builder.Services.AddScoped<ICredentialAccessor, CredentialAccessor>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IProfilePictureRepository, ProfilePictureRepository>();
builder.Services.AddScoped<IContactInformationRepository, ContactInformationRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAuthorization, Authorization>();

var server = builder.Build();
using (var scope = server.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();
    context.Database.Migrate();
}

server.UseMiddleware<ApiExceptionMiddleware>();
server.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
server.UseHttpsRedirection();
server.UseAuthentication();
server.UseAuthorization();
server.MapControllers();
server.Run();

public partial class Program
{
}