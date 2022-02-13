using System.Net;
using API.Exceptions;
using API.Repositories.Insurance;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Insurance;

public class CreateInsurance
{
    public class Command : IRequest<Unit>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAuthorization _authorization;
        private readonly IInsuranceRepository _repository;

        public Handler(IInsuranceRepository repository, IAuthorization authorization)
        {
            _repository = repository;
            _authorization = authorization;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdmin = await _authorization.IsAdministrator();
            if (!isAdmin)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );
            var insurance = new Models.Insurance
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            await _repository.Save(insurance, cancellationToken);
            return Unit.Value;
        }
    }
}