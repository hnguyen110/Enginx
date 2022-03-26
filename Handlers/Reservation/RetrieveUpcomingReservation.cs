using API.DTOs.Reservation;
using API.Handlers.Vehicle;
using API.Repositories.Reservation;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.Reservation;

public class RetrieveUpcomingReservation
{
    public class Query : IRequest<RetrieveUpcomingReservationDTO?>
    {
    }

    public class Handler : IRequestHandler<Query, RetrieveUpcomingReservationDTO?>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IReservationRepository _reservation;

        public Handler(ICredentialAccessor accessor, IMapper mapper, IReservationRepository reservation,
            IMediator mediator)
        {
            _accessor = accessor;
            _mapper = mapper;
            _reservation = reservation;
            _mediator = mediator;
        }

        public async Task<RetrieveUpcomingReservationDTO?> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await
                _reservation
                    .RetrieveUpcomingReservationByAccountId(
                        _accessor.RetrieveAccountId(),
                        cancellationToken
                    );

            if (record == null) return null;

            var result = _mapper
                .Map<Models.Reservation, RetrieveUpcomingReservationDTO>(record);
            var pictures = await _mediator
                .Send(
                    new RetrieveVehiclePictures.Query {Id = record.Vehicle},
                    cancellationToken
                );
            result.VehiclePictures = pictures;
            return result;
        }
    }
}