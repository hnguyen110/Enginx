using API.DTOs.Insurance;
using API.Handlers.Insurance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InsuranceController : BaseController
{
    [HttpPost("create-insurance")]
    public async Task<CreateInsuranceDTO> CreateInsurance(CreateInsurance.Command command)
    {
        return await Mediator!.Send(command);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<RetrieveAllInsurancesDTO>> RetrieveAllInsurances()
    {
        return await Mediator!.Send(new RetrieveAllInsurances.Query());
    }

    [HttpPut("update-insurance/{id}")]
    public async Task<Unit> UpdateInsurance(string id, UpdateInsurance.Command command)
    {
        command.Id = id;
        return await Mediator!.Send(command);
    }

    [HttpDelete("delete-insurance/{id}")]
    public async Task<Unit> DeleteInsurance(string id)
    {
        return await Mediator!.Send(new DeleteInsurance.Command {Id = id});
    }
}