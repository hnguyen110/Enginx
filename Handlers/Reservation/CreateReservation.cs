using API.Repositories.Reservation;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.Reservation;

public class CreateReservation
{
    public class Command : IRequest<Unit>
    {
        public DateTime? Date { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }
        public string? Transaction { get; set; }
        public string? Vehicle { get; set; }
        public string? Insurance { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IReservationRepository _repository;

        public Handler(IReservationRepository repository, ICredentialAccessor accessor)
        {
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var card = new Models.Reservation
            {
                Id = Guid.NewGuid().ToString(),
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                CheckInTime = request.CheckInTime,
                CheckOutTime = request.CheckOutTime,
                Date = request.Date,
                Transaction = request.Transaction,
                Vehicle = request.Vehicle,
                Insurance = request.Insurance
            };

            await _repository.Save(card, cancellationToken);
            return Unit.Value;
        }
    }
}