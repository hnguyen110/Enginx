using System.Net;
using API.Exceptions;
using API.Handlers.Reservation;
using API.Handlers.Transaction;
using API.Repositories.BankCard;
using API.Repositories.Insurance;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Checkout;

public class CreateCheckout
{
    public class Command : IRequest<Unit>
    {
        public string? Vehicle { get; set; }
        public string? Insurance { get; set; }
        public string? BankCard { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IBankCardRepository _bankCardRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMediator _mediator;
        private readonly IVehicleRepository _vehicleRepository;

        public Handler(
            IMediator mediator,
            ICredentialAccessor accessor,
            IVehicleRepository vehicleRepository,
            IInsuranceRepository insuranceRepository,
            IBankCardRepository bankCardRepository)
        {
            _mediator = mediator;
            _accessor = accessor;
            _vehicleRepository = vehicleRepository;
            _insuranceRepository = insuranceRepository;
            _bankCardRepository = bankCardRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var sender = _accessor.RetrieveAccountId();
            var vehicle = await
                _vehicleRepository
                    .RetrievedPublishedVehicleById(
                        request.Vehicle,
                        cancellationToken
                    );
            if (vehicle == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            var bankcard = await
                _bankCardRepository
                    .RetrieveBankCardById(
                        request.BankCard,
                        cancellationToken
                    );
            if (bankcard == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            var insurance = await
                _insuranceRepository
                    .RetrieveInsuranceById(
                        request.Insurance,
                        cancellationToken
                    );
            if (insurance == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            var duration = request.CheckOutDate - request.CheckInDate;
            var total = vehicle.Price * duration!.Value.TotalDays + insurance.Price;
            var transaction = await _mediator.Send(new CreateTransaction.Command
            {
                Amount = total,
                Receiver = vehicle.Owner
            }, cancellationToken);

            await _mediator.Send(new CreateReservation.Command
            {
                Date = DateTime.Today,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                CheckInTime = request.CheckInTime,
                CheckOutTime = request.CheckOutTime,
                Transaction = transaction,
                Vehicle = vehicle.Id,
                Insurance = insurance.Id
            }, cancellationToken);

            return Unit.Value;
        }
    }
}