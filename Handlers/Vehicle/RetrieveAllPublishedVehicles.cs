using API.DTOs.Vehicle;
using API.Repositories.Vehicle;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveAllPublishedVehicles
{
    public class Query : IRequest<List<RetrieveAllVehiclesDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllVehiclesDTO>>

    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<List<RetrieveAllVehiclesDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _repository.RetrieveAllPublishedVehicles(cancellationToken);

            return _mapper.Map<List<Models.Vehicle>, List<RetrieveAllVehiclesDTO>>(records);
        }
    }
}