using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Vehicle;

public class DeleteVehicleByAdministrator
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAuthorization _authorization;
        private readonly IVehicleRepository _vehicle;

        public Handler(IAuthorization authorization, IVehicleRepository vehicle)
        {
            _authorization = authorization;
            _vehicle = vehicle;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var record = await _vehicle
                .RetrieveVehicleById(
                    request.Id,
                    cancellationToken
                );
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _vehicle.DeleteVehicle(record, cancellationToken);
            return Unit.Value;
        }
    }
}