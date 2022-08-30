using API.Handlers.Checkout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CheckoutController : BaseController
{
    [HttpPost("create-checkout")]
    public async Task<Unit> CreateCheckout(CreateCheckout.Command command)
    {
        return await Mediator!.Send(command);
    }
}