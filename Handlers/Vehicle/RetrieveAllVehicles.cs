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
        private readonly IVehicleRepository _vehicle;

        public Command(IAuthorization authorization, IMapper mapper, IVehicleRepository vehicle)
        {
            _authorization = authorization;
            _mapper = mapper;
            _vehicle = vehicle;
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
            return _mapper.Map<List<Models.Vehicle>, List<RetrieveAllVehiclesDTO>>(records);
        }
    }
}