using System.Net;
using System.Security.Principal;
using API.Exceptions;
using API.Handlers.Account;
using API.Handlers.Address;
using API.Handlers.ContactInformation;
using API.Repositories.Account;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Profile;

public class UpdateProfileInformation
{
    public class Command : IRequest<Unit>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMediator _mediator;
        private readonly IAccountRepository _repository;

        public Handler(IMediator mediator, ICredentialAccessor accessor, IAccountRepository repository)
        {
            _mediator = mediator;
            _accessor = accessor;
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var account = await _repository
                .FindById(
                    _accessor.RetrieveAccountId()!,
                    cancellationToken
                );
            if (account == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _mediator.Send(new UpdateContactInformation.Command
            {
                Id = account.ContactInformation,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                ContactNumber = request.ContactNumber
            }, cancellationToken);
            
            await _mediator.Send(new UpdateAddress.Command
            {
                Id = account.Address,
                StreetNumber = request.StreetNumber,
                StreetName = request.StreetName,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode,
            }, cancellationToken);
            
            return Unit.Value;
        }
    }
}