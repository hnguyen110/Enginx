using Microsoft.AspNetCore.Mvc;
using API.Handlers.Reservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers;

// [AllowAnonymous]
public class ReservationController : BaseController
{
    [HttpPost("create-reservation")]

    // public async Task<string> CreateReservation.Command command)
    // {
    //     return await MediatR.Mediator!.Send(command)
    // }

    public async Task<Unit> CreateReservation(CreateReservation.Command command)
    {
        return await Mediator!.Send(command);
    }

// [AllowAnonymous]
// public class AddressController : BaseController
// {
//     [HttpPost("create-address")]
//     public async Task<string> CreateAddress(CreateReservation.Command command)
//     {
//         return await Mediator!.Send(command);
//     }
// }
}