using System.Net;
using API.Exceptions;
using API.Repositories.Reservation;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Reservation;

public class CancelUpcomingReservation
{
    public class Command : IRequest<Unit>
    {
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IReservationRepository _reservation;

        public Handler(ICredentialAccessor accessor, IReservationRepository reservation)
        {
            _accessor = accessor;
            _reservation = reservation;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record =
                await _reservation
                    .RetrieveUpcomingReservationByAccountId(
                        _accessor.RetrieveAccountId(),
                        cancellationToken
                    );
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            await _reservation.UpdateUpcomingReservationStatus(record, false, cancellationToken);
            return Unit.Value;
        }
    }
}