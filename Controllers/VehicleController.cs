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
    
    [HttpPost("vehicle-picture/{id}")]
    [RequestSizeLimit(int.MaxValue)]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue,
        MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<Unit> UploadVehiclePicture([FromForm] UploadVehiclePicture.Command command, string id)
    {
        command.Id = id;
        return await Mediator!.Send(command);
    }
    
}
