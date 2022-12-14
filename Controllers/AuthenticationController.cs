using API.DTOs.Authentication;
using API.Handlers.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class AuthenticationController : BaseController
{
    [HttpPost("sign-in")]
    public async Task<JwtToken> SignIn(SignIn.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpPost("sign-up")]
    public async Task<SignUpDTO> SignIn(SignUp.Command command)
    {
        return await Mediator!.Send(command);
    }
}