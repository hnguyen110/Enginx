using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Vehicle;

public class DeleteVehicle
{
    public class Query : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, ICredentialAccessor accessor)
        {
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository.RetrieveVehicleById(
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