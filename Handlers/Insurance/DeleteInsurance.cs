using System.Net;
using API.Exceptions;
using API.Repositories.Insurance;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Insurance;

public class DeleteInsurance
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAuthorization _authorization;
        private readonly IInsuranceRepository _insurance;

        public Handler(IAuthorization authorization, IInsuranceRepository insurance)
        {
            _authorization = authorization;
            _insurance = insurance;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var insurance = await _insurance
                .RetrieveInsuranceById(
                    request.Id,
                    cancellationToken
                );
            if (insurance == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _insurance
                .DeleteInsurance(
                    insurance,
                    cancellationToken
                );
            return Unit.Value;
        }
    }
}