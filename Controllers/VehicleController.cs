using API.DTOs.Vehicle;
using API.DTOs.VehiclePicture;
using API.Handlers.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        return await Mediator!.Send(new RetrieveVehiclePicture.Query { Id = id });
    }


    [HttpGet("{id}")]
    public async Task<RetrieveVehicleDTO> RetrieveVehicle(string id)
    {
        return await Mediator!.Send(new RetrieveVehicle.Query { Id = id });
    }

    [HttpGet]
    public async Task<List<RetrieveAllVehicleDTO>> RetrieveAllVehicles()
    {
        return await Mediator!.Send(new RetrieveAllVehicles.Query());
    }
    
    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<List<RetrieveVehicleDTO>> RetrieveVehiclesByLocation([FromQuery(Name = "location")]string? location)
    {
        return await Mediator!.Send(new RetrieveVehiclesByLocation.Query {Location = location});
    }
}