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
        private readonly IMediator _mediator;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
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

            var pictures = await _mediator.Send(
                new RetrieveVehiclePictures.Query {Id = request.Id},
                cancellationToken
            );

            var vehicle = _mapper.Map<Models.Vehicle, RetrieveVehicleDTO>(record);
            vehicle.Pictures = pictures;

            return vehicle;
        }
    }
}