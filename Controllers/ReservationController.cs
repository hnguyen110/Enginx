using Microsoft.AspNetCore.Mvc;
using API.Handlers.Reservation;
using API.DTOs.Reservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers;

// [AllowAnonymous]
public class ReservationController : BaseController
{
    [HttpPost("create-reservation")]
    public async Task<Unit> CreateReservation(CreateReservation.Command command)
    {
        return await Mediator!.Send(command);
    }
    
    [HttpGet("{Id}")]
    public async Task<List<RetrieveAllReservationsDTO>> RetrieveAllReservation(string Id)
    {
        return await Mediator!.Send(new RetrieveAllReservation.Query());
    }
    
}