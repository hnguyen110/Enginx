using API.Handlers.Transaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TransactionController : BaseController
{
    [HttpPost("create-transaction")]
    public async Task<Unit> CreateTransaction(CreateTransaction.Command command)
    {
        return await Mediator!.Send(command);
    }
}