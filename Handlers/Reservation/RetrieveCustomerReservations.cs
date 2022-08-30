using API.DTOs.Reservation;
using API.Repositories.Reservation;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.Reservation;

public class RetrieveClientReservation
{
    public class Query : IRequest<List<RetrieveCustomerReservationsDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveCustomerReservationsDTO>>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _repository;

        public Handler(IMapper mapper, IReservationRepository repository, ICredentialAccessor accessor)
        {
            _mapper = mapper;
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<List<RetrieveCustomerReservationsDTO>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var id = _accessor.RetrieveAccountId();

            var reservations = await _repository
                .RetrieveAllClientReservations(
                    id,
                    cancellationToken
                );

            return _mapper.Map<List<Models.Reservation>, List<RetrieveCustomerReservationsDTO>>(reservations);
        }
    }
}