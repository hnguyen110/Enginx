using System.Net;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.Messages;
using API.Utilities.Security;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveAllVehicles
{
    public class Query : IRequest<List<RetrieveAllVehiclesDTO>>
    {
    }

    public class Command : IRequestHandler<Query, List<RetrieveAllVehiclesDTO>>
    {
        private readonly IAuthorization _authorization;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IVehicleRepository _vehicle;

        public Command(IAuthorization authorization, IMapper mapper, IVehicleRepository vehicle, IMediator mediator)
        {
            _authorization = authorization;
            _mapper = mapper;
            _vehicle = vehicle;
            _mediator = mediator;
        }

        public async Task<List<RetrieveAllVehiclesDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var records = await _vehicle.RetrieveAllVehicles(cancellationToken);
            var vehicles = new List<RetrieveAllVehiclesDTO>();

            foreach (var vehicle in records)
            {
                var vp = await _mediator.Send(new RetrieveVehiclePictures.Query {Id = vehicle.Id}
                    , cancellationToken
                );

                vehicles.Add(new RetrieveAllVehiclesDTO
                {
                    Id = vehicle.Id,
                    BodyType = vehicle.BodyType,
                    Color = vehicle.Color,
                    Description = vehicle.Description,
                    EngineType = vehicle.EngineType,
                    FuelType = vehicle.FuelType,
                    Location = vehicle.Location,
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Mileage = vehicle.Mileage,
                    Price = vehicle.Price,
                    Year = vehicle.Year,
                    VehiclePictures = vp
                });
            }

            return vehicles;
        }
    }
}