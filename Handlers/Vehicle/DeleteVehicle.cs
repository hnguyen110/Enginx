using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Vehicle;

public class DeleteVehicle
{
    public class Query : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Unit>
    {
        private readonly IAuthorization _authorization;
        private readonly IVehicleRepository _repository;
        private readonly ICredentialAccessor _accessor;
        
        public Handler(IAuthorization authorization, IVehicleRepository repository, ICredentialAccessor accessor)
        {
            _authorization = authorization;
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var isOwner = await _authorization.IsOwner();
            
            if (!isOwner)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );
            
            var record = await _repository.
                RetrieveVehicleById(
                    _accessor.RetrieveAccountId(),
                    request.Id,
                    cancellationToken
                );
            
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound, 
                    ApiErrorMessages.NotFound
                );

            await _repository.DeleteVehicle(record, cancellationToken);
            return Unit.Value;
        }
    }
}