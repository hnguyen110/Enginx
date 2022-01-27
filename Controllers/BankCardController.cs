using API.Handlers.BankCard;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class BankCardController : BaseController
{
    [HttpPost]
    public async Task<BankCard> CreateBankCard(CreateBankCard.Command command)
    {
        return await Mediator!.Send(command);
    }
    
}