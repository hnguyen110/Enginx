using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Handlers.Address;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[AllowAnonymous]
public class AddressController: BaseController
{
    [HttpPost]
    public async Task<Unit> CreateAddress(CreateAddress.Command command)
    {
        return await Mediator!.Send(command);
    }
    
}