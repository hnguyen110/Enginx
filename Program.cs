using System.Text;
using API.DatabaseContext;
using API.Handlers.Authentication;
using API.MappingProfile;
using API.Middlewares;
using API.Repositories.Account;
using API.Repositories.Address;
using API.Repositories.BankCard;
using API.Repositories.ContactInformation;
using API.Repositories.Profile;
using API.Repositories.Reservation;
using API.Repositories.Vehicle;
using API.Repositories.VehiclePicture;
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
using Newtonsoft.Json;

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
        configuration.RegisterValidatorsFromAssemblyContaining<SignIn.CommandValidator>())
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
builder.Services.AddAutoMapper(typeof(MappingProfile));
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
builder.Services.AddScoped<IBankCardRepository, BankCardRepository>();
builder.Services.AddScoped<IProfilePictureRepository, ProfilePictureRepository>();
builder.Services.AddScoped<IContactInformationRepository, ContactInformationRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAuthorization, Authorization>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

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