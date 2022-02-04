using API.DTOs.VehiclePicture;
using API.Handlers.Address;
using API.Handlers.Vehicle;
using API.Models;
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

    [HttpGet("vehicle-picture/{id}")]
    public async Task<List<RetrieveVehiclePicturesDTO>> RetrieveVehiclePictureById(string id)
    {
        return await Mediator!.Send(new RetrieveVehiclePicture.Query{ Id = id});
    }
    
}
