using System.Net;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveVehicle
{
    public class Query : IRequest<RetrieveVehicleDTO>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, RetrieveVehicleDTO>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public Handler(ICredentialAccessor accessor, IVehicleRepository repository, IMapper mapper)
        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RetrieveVehicleDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .RetrieveVehicleById(
                    _accessor.RetrieveAccountId(), request.Id,
                    cancellationToken
                );
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            return _mapper.Map<Models.Vehicle, RetrieveVehicleDTO>(record);
        }
    }
}