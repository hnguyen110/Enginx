using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Vehicle;

public class RejectVehicle
{
    public class Query : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Unit>
    {
        private readonly IAuthorization _authorization;
        private readonly IVehicleRepository _repository;


        public Handler(IAuthorization authorization, IVehicleRepository repository)
        {
            _authorization = authorization;
            _repository = repository;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var record = await _repository
                .RetrieveVehicleById(
                    request.Id,
                    cancellationToken
                );
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _repository.RejectVehicle(record, cancellationToken);
            return Unit.Value;
        }
    }
}