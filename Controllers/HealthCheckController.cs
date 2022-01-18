using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class HealthCheckController : BaseController
{
    [HttpGet]
    public async Task<Unit> HealthCheck()
    {
        return await Task.FromResult(Unit.Value);
    }
}