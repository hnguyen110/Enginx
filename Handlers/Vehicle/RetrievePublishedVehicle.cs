using System.Net;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrievePublishedVehicle
{
    public class Query : IRequest<RetrieveVehicleDTO>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, RetrieveVehicleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RetrieveVehicleDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .RetrievePublishedVehicleById(
                    request.Id,
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