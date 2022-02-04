using API.DTOs.Vehicle;
using API.Handlers.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class VehicleController : BaseController
{
    [HttpPost("create-vehicle")]
    public async Task<Unit> CreateVehicle(CreateVehicle.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<RetrieveVehicleDTO> RetrieveVehicle(string id)
    {
        return await Mediator!.Send(new RetrieveVehicle.Query { Id = id });
    }
}