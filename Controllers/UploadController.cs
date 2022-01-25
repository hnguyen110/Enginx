using API.Handlers.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class UploadController : BaseController
{
    [HttpPost]
    [DisableRequestSizeLimit]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<Unit> Upload([FromForm] UploadProfilePicture.Command command)
    {
        return await Mediator!.Send(command);
    }
}