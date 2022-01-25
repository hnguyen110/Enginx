using API.Handlers.Account;
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
}