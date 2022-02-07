using System.Net;
using API.DatabaseContext;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;


namespace API.Handlers.Vehicle;
public class RetrieveAllVehicles
{
    public class Query : IRequest<List<RetrieveVehicleDTO>>

    {
        
    }
    
    public class Handler : IRequestHandler<Query, List<RetrieveVehicleDTO>>

    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;
        private readonly Context _database;
        
        public Handler(Context database, ICredentialAccessor accessor, IVehicleRepository repository, IMapper mapper)

        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
            _database = database;
        }


        public async Task<List<RetrieveVehicleDTO>> Handle(Query request, CancellationToken cancellationToken)

        {
        
            var owner = _accessor.RetrieveAccountId();
            var records = await _repository.RetrieveAllVehicles(owner, cancellationToken);

            return _mapper.Map<List<Models.Vehicle>, List<RetrieveVehicleDTO>>(records);
        }
    }
}