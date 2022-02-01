using API.Handlers.Address;
using API.Handlers.ContactInformation;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class CIController : BaseController
{
    [HttpGet("contact-information/{id}")]
    public async Task<ContactInformation> RetrieveContactInformation(string id)
    {
        return await Mediator!.Send(new RetrieveContactInformation.Query {Id = id});
    }

    [HttpGet("address/{id}")]
    public async Task<Address> RetrieveAddress(string id)
    {
        return await Mediator!.Send(new RetrieveAddress.Query {Id = id});
    }
}