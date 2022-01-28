using API.Handlers.BankCard;
using API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class BankCardController : BaseController
{
    [HttpPost]
    public async Task<Unit> CreateBankCard(CreateBankCard.Command command)
    {
        return await Mediator!.Send(command);
    }
    
}