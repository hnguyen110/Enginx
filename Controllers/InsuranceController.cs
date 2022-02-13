using API.Handlers.Insurance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InsuranceController : BaseController
{
    [HttpPost("create-insurance")]
    public async Task<Unit> CreateInsurance(CreateInsurance.Command command)
    {
        return await Mediator!.Send(command);
    }
}