using System.Net;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class PublishVehicle
{
    public class Command : IRequest<PublishVehicleDTO>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, PublishVehicleDTO>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicle;

        public Handler(ICredentialAccessor accessor, IMapper mapper, IVehicleRepository vehicle)
        {
            _accessor = accessor;
            _mapper = mapper;
            _vehicle = vehicle;
        }

        public async Task<PublishVehicleDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicle
                .RetrieveVehicleById(
                    _accessor.RetrieveAccountId(),
                    request.Id,
                    cancellationToken
                );
            if (vehicle == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _vehicle.PublishVehicle(vehicle, cancellationToken);
            return _mapper.Map<Models.Vehicle, PublishVehicleDTO>(vehicle);
        }
    }
}