using API.DTOs.Account;
using API.Handlers.Account;
using API.Handlers.Profile;
using API.Models;
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

    [HttpGet("retrieve-all-client-accounts")]
    public async Task<List<RetrieveAllClientAccountDTO>> RetrieveAllClientAccounts()
    {
        return await Mediator!.Send(new RetrieveAllClientAccounts.Query());
    }

    [HttpPut("approve-account/{id}")]
    public async Task<Unit> ApproveAccount(string id)
    {
        return await Mediator!.Send(new ApproveAccount.Command {Id = id});
    }


    [HttpPut("disapprove-account/{id}")]
    public async Task<Unit> DisapproveAccount(string id)
    {
        return await Mediator!.Send(new DisapproveAccount.Command {Id = id});
    }

    [HttpDelete("delete-client-account/{id}")]
    public async Task<Unit> DeleteClientAccount(string id)
    {
        return await Mediator!.Send(new DeleteClientAccount.Command {Id = id});
    }
}