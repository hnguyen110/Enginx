using API.DTOs.BankCard;
using API.Handlers.BankCard;
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
    public async Task<List<RetrieveAllBankCardsDTO>> RetrieveAllBankCardsByAccount()
    {
        return await Mediator!.Send(new RetrieveAllBankCards.Query());
    }
}