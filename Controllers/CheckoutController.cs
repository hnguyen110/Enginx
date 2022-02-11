using API.Handlers.Checkout;
using API.Handlers.Transaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CheckoutController : BaseController
{
    [HttpPost]
    public async Task<Unit> CreateCheckout(CreateCheckout.Command command)
    {
        return await Mediator!.Send(command);
    }
}