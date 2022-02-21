using API.DTOs.Reservation;
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
        private readonly IReservationRepository _reservation;

        public Handler(ICredentialAccessor accessor, IMapper mapper, IReservationRepository reservation)
        {
            _accessor = accessor;
            _mapper = mapper;
            _reservation = reservation;
        }

        public async Task<RetrieveUpcomingReservationDTO?> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await
                _reservation
                    .RetrieveUpcomingReservationByAccountId(
                        _accessor.RetrieveAccountId(),
                        cancellationToken
                    );
            return record == null
                ? null
                : _mapper.Map<
                    Models.Reservation,
                    RetrieveUpcomingReservationDTO
                >(record);
        }
    }
}