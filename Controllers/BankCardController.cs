using API.Handlers.BankCard;
using API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BankCardController : BaseController
{
    [HttpPost]
    public async Task<Unit> CreateBankCard(CreateBankCard.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpGet]
    public async Task<List<BankCard>> RetrieveAllBankCardsByAccount()
    {
        return await Mediator!.Send(new RetrieveAllBankCards.Query());
    }
}