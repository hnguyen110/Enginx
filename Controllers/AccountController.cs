using API.Handlers.Account;
using API.Handlers.Profile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseController
{
    [HttpPost("profile-picture")]
    [RequestSizeLimit(int.MaxValue)]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue,
        MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<Unit> UploadProfilePicture([FromForm] UploadProfilePicture.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpPost("change-password")]
    public async Task<Unit> ChangePassword(ChangePassword.Command command)
    {
        return await Mediator!.Send(command);
    }
}