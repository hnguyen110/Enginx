using API.DTOs.Profile;
using API.Handlers.Profile;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : BaseController
{
    [HttpGet("profile-picture")]
    public async Task<string?> RetrieveProfilePicture(string? id)
    {
        return await Mediator!.Send(new RetrieveProfilePicture.Query { Id = id });
    }

    [HttpGet("profile-information")]
    public async Task<RetrieveProfileDTO> RetrieveProfileInformation()
    {
        return await Mediator!.Send(new RetrieveProfileInformation.Query());
    }
}