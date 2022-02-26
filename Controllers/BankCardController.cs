using API.DTOs.BankCard;
using API.Handlers.BankCard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BankCardController : BaseController
{
    [HttpPost("create-bankcard")]
    public async Task<Unit> CreateBankCard(CreateBankCard.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpGet]
    public async Task<List<RetrieveAllBankCardsDTO>> RetrieveAllBankCardsByAccount()
    {
        return await Mediator!.Send(new RetrieveAllBankCards.Query());
    }

    [HttpPut("update-bankcard/{id}")]
    public async Task<Unit> UpdateBankCard(string id, UpdateBankCard.Command command)

    {
        command.Id = id;
        return await Mediator!.Send(command);
    }

    [HttpDelete("delete-bankcard/{id}")]
    public async Task<Unit> DeleteBankCard(string id)
    {
        return await Mediator!.Send(new DeleteBankCard.Query { Id = id });
    }
}