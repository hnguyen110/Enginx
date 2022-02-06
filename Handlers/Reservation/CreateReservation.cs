using API.DatabaseContext;
using API.Handlers.Address;
using MediatR;
using API.Handlers.Reservation;
using API.Repositories.Reservation;
using API.Utilities.CredentialAccessor;

namespace API.Handlers.Reservation;

public class CreateReservation
{
    public class Command : IRequest<Unit>
    {
        public string? Date { get; set; }
        public string? CheckInDate { get; set; }
        public string? CheckOutDate { get; set; }
        public string? CheckInTime { get; set; }
        public string? CheckOutTime { get; set; }
        
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
            var card = new Models.Reservation()
            {
                Id = Guid.NewGuid().ToString(),
                // Account = _accessor.RetrieveAccountId(),
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                CheckInTime = request.CheckInTime,
                CheckOutTime = request.CheckOutTime,
                Date = request.Date
                    
            };

            await _repository.Save(card, cancellationToken);
            return Unit.Value;
        }
    }
}