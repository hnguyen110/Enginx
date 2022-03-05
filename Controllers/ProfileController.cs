using API.DTOs.Profile;
using API.Handlers.Profile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : BaseController
{
    [HttpGet("profile-picture")]
    public async Task<string?> RetrieveProfilePicture(string? id)
    {
        return await Mediator!.Send(new RetrieveProfilePicture.Query {Id = id});
    }

    [HttpGet("profile-information")]
    public async Task<RetrieveProfileDTO> RetrieveProfileInformation()
    {
        return await Mediator!.Send(new RetrieveProfileInformation.Query());
    }

    [HttpPut("update-profile-information")]
    public async Task<Unit> UpdateProfile(UpdateProfileInformation.Command command)
    {
        return await Mediator!.Send(command);
    }
}