using API.DTOs.Review;
using API.DTOs.Vehicle;
using API.Handlers.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class VehicleController : BaseController
{
    [HttpPost("create-vehicle")]
    public async Task<RetrieveVehicleDTO> CreateVehicle(CreateVehicle.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpPost("vehicle-picture/{id}")]
    [RequestSizeLimit(int.MaxValue)]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue,
        MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<Unit> UploadVehiclePictures([FromForm] UploadVehiclePicture.Command command, string id)
    {
        command.Id = id;
        return await Mediator!.Send(command);
    }

    [HttpGet("vehicle-picture/{id}")]
    public async Task<List<string>> RetrieveVehiclePictureById(string id)
    {
        return await Mediator!.Send(new RetrieveVehiclePictures.Query { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<RetrieveVehicleDTO> RetrieveVehicle(string id)
    {
        return await Mediator!.Send(new RetrieveVehicle.Query { Id = id });
    }

    [HttpPost("create-review/{id}")]
    public async Task<CreateVehicleReviewDTO> CreateVehicleReview(CreateReview.Command command, string id)
    {
        command.Vehicle = id;
        return await Mediator!.Send(command);
    }

    [HttpGet("retrieve-all-vehicles")]
    public async Task<List<RetrieveAllVehiclesDTO>> RetrieveAllVehicles()
    {
        return await Mediator!.Send(new RetrieveAllVehicles.Query());
    }

    [HttpGet("retrieve-all-vehicles-by-owner")]
    public async Task<List<RetrieveAllVehiclesDTO>> RetrieveAllVehiclesByOwnerId()
    {
        return await Mediator!.Send(new RetrieveAllVehiclesByOwnerId.Query());
    }

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<List<RetrieveVehicleDTO>> RetrieveVehiclesByLocation(
        [FromQuery(Name = "location")] string? location)
    {
        return await Mediator!.Send(new RetrieveVehiclesByLocation.Query { Location = location });
    }

    [AllowAnonymous]
    [HttpGet("published-vehicle/{id}")]
    public async Task<RetrieveVehicleDTO> RetrievePublishedVehicle(string id)
    {
        return await Mediator!.Send(new RetrievePublishedVehicle.Query { Id = id });
    }

    [AllowAnonymous]
    [HttpGet("published-vehicles")]
    public async Task<List<RetrieveAllVehiclesDTO>> RetrieveAllPublishedVehicles()
    {
        return await Mediator!.Send(new RetrieveAllPublishedVehicles.Query());
    }

    [AllowAnonymous]
    [HttpGet("reviews/{id}")]
    public async Task<List<RetrieveAllReviewsDTO>> RetrieveAllVehicleReviews(string id)
    {
        return await Mediator!.Send(new RetrieveAllReviews.Query { Id = id });
    }

    [HttpPut("update-vehicle/{id}")]
    public async Task<Unit> UpdateVehicleInformation(UpdateVehicleInformation.Command command, string id)
    {
        command.Id = id;
        return await Mediator!.Send(command);
    }

    [HttpPut("approve-vehicle/{id}")]
    public async Task<Unit> ApproveVehicle(string id)
    {
        return await Mediator!.Send(new ApproveVehicle.Query { Id = id });
    }

    [HttpPut("reject-vehicle/{id}")]
    public async Task<Unit> RejectVehicle(string id)
    {
        return await Mediator!.Send(new RejectVehicle.Query { Id = id });
    }

    [HttpPut("publish-vehicle/{id}")]
    public async Task<PublishVehicleDTO> PublishVehicle(string id)
    {
        return await Mediator!.Send(new PublishVehicle.Command { Id = id });
    }

    [HttpPut("hide-vehicle/{id}")]
    public async Task<HideVehicleDTO> HideVehicle(string id)
    {
        return await Mediator!.Send(new HideVehicle.Command { Id = id });
    }

    [HttpDelete("delete-vehicle/{id}")]
    public async Task<Unit> DeleteVehicleByOwner(string id)
    {
        return await Mediator!.Send(new DeleteVehicle.Query { Id = id });
    }

    [HttpDelete("delete-vehicle-by-administrator/{id}")]
    public async Task<Unit> DeleteVehicleByAdministrator(string id)
    {
        return await Mediator!.Send(new DeleteVehicleByAdministrator.Command { Id = id });
    }
}