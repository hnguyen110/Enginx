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
}