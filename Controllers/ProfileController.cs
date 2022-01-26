using API.DTOs.Profile;
using API.Handlers.Profile;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : BaseController
{
    [HttpGet("profile-picture")]
    public async Task<string> RetrieveProfilePicture(string? id)
    {
        var path = await Mediator!.Send(new RetrieveProfilePicture.Query {Id = id});
        return path == null ? "" : Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync(path));
    }

    [HttpGet("profile-information")]
    public async Task<RetrieveProfileDTO> RetrieveProfileInformation()
    {
        return await Mediator!.Send(new RetrieveProfileInformation.Query());
    }
}