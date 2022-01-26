using API.DTOs.Profile;
using API.Handlers.Profile;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : BaseController
{
    [HttpGet("profile-information")]
    public async Task<RetrieveProfileDTO> RetrieveProfileInformation()
    {
        return await Mediator!.Send(new RetrieveProfileInformation.Query());
    }
}