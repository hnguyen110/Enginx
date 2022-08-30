using API.DTOs.Reservation;
using API.Repositories.Reservation;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.Reservation;

public class RetrieveAllReservation
{
    public class Query : IRequest<List<RetrieveAllReservationsDTO>>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllReservationsDTO>>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _repository;

        public Handler(ICredentialAccessor accessor, IReservationRepository repository, IMapper mapper)
        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RetrieveAllReservationsDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _repository
                .RetrieveAllReservationsByVehicleId(
                    request.Id,
                    cancellationToken
                );
            return _mapper
                .Map<List<Models.Reservation>, List<RetrieveAllReservationsDTO>>(records);
        }
    }
}