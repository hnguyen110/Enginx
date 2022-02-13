using API.DTOs.Insurance;
using API.Handlers.Insurance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InsuranceController : BaseController
{
    [HttpPost("create-insurance")]
    public async Task<Unit> CreateInsurance(CreateInsurance.Command command)
    {
        return await Mediator!.Send(command);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<RetrieveAllInsurancesDTO>> RetrieveAllInsurances()
    {
        return await Mediator!.Send(new RetrieveAllInsurances.Query());
    }
}