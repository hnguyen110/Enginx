using API.Handlers.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class AddressController : BaseController
{
    [HttpPost]
    public async Task<string> CreateAddress(CreateAddress.Command command)
    {
        return await Mediator!.Send(command);
    }
}