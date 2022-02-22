using System.Net;
using API.Exceptions;
using API.Repositories.Insurance;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Insurance;

public class UpdateInsurance
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
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

            var updates = new Models.Insurance
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            await _insurance
                .Update(
                    insurance,
                    updates,
                    cancellationToken
                );
            return Unit.Value;
        }
    }
}