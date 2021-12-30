using API.Handlers.ContactInformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class CIController : BaseController
{
    [HttpPost("contact-information")]
    public async Task<string> SignIn(CreateContactInformation.Command command)
    {
        return await Mediator!.Send(command);
    }
}