using API.DTOs.Reservation;
using API.Handlers.Reservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ReservationController : BaseController
{
    [HttpPost("create-reservation")]
    public async Task<Unit> CreateReservation(CreateReservation.Command command)
    {
        return await Mediator!.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<List<RetrieveAllReservationsDTO>> RetrieveAllReservation(string id)
    {
        return await Mediator!.Send(new RetrieveAllReservation.Query());
    }

    [HttpDelete("{id}")]
    public async Task<Unit> DeleteReservation(string? id)
    {
        return await Mediator!.Send(new DeleteReservation.Command {Id = id});
    }
}