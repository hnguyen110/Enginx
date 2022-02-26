using API.DatabaseContext;
using API.DTOs.Vehicle;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveAllVehiclesByOwnerId
{
    public class Query : IRequest<List<RetrieveAllVehiclesDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllVehiclesDTO>>

    {
        private readonly ICredentialAccessor _accessor;
        private readonly Context _database;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public Handler(Context database, ICredentialAccessor accessor, IVehicleRepository repository, IMapper mapper)

        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
            _database = database;
        }


        public async Task<List<RetrieveAllVehiclesDTO>> Handle(Query request, CancellationToken cancellationToken)

        {
            var owner = _accessor.RetrieveAccountId();
            var records = await _repository.RetrieveAllVehiclesByOwnerId(owner, cancellationToken);

            return _mapper.Map<List<Models.Vehicle>, List<RetrieveAllVehiclesDTO>>(records);
        }
    }
}