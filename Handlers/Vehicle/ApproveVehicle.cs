using System.Net;
using API.Exceptions;
using API.Handlers.Account;
using API.Repositories.Vehicle;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Vehicle;

public class ApproveVehicle
{
    public class Query : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Unit>
    {
        private readonly IVehicleRepository _repository;
        private readonly IAuthorization _authorization;

        public Handler(IVehicleRepository repository, IAuthorization authorization)
        {
            _repository = repository;
            _authorization = authorization;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var isAdmin = await _authorization.IsAdministrator();
            if (!isAdmin)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );
            await _repository.ApproveVehicle(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
    
}