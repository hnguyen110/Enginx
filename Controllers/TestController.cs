using API.Handlers.Test;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class TestController : BaseController
{
    [HttpDelete("reset-database")]
    public async Task<Unit> ResetDatabase()
    {
        return await Mediator!.Send(new ResetDatabase.Command());
    }
}