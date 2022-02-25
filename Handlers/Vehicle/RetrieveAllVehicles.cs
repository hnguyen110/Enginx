using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveAllVehicles
{
    public class Query : IRequest<List<Models.Vehicle>>
    {
    }

    public class Command : IRequestHandler<Query, List<Models.Vehicle>>
    {
        private readonly IAuthorization _authorization;
        private readonly IVehicleRepository _vehicle;

        public Command(IAuthorization authorization, IVehicleRepository vehicle)
        {
            _authorization = authorization;
            _vehicle = vehicle;
        }

        public async Task<List<Models.Vehicle>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var records = await _vehicle.RetrieveAllVehicles(cancellationToken);
            return records;
        }
    }
}