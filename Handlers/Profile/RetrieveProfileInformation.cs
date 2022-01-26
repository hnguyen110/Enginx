using System.Net;
using API.DTOs.Profile;
using API.Exceptions;
using API.Handlers.Address;
using API.Handlers.ContactInformation;
using API.Repositories.Account;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Profile;

public class RetrieveProfileInformation
{
    public class Query : IRequest<RetrieveProfileDTO>
    {
    }

    public class Handler : IRequestHandler<Query, RetrieveProfileDTO>
    {
        private readonly IMediator _mediator;
        private readonly ICredentialAccessor _accessor;
        private readonly IAccountRepository _repository;

        public Handler(IMediator mediator, ICredentialAccessor accessor, IAccountRepository repository)
        {
            _mediator = mediator;
            _accessor = accessor;
            _repository = repository;
        }

        public async Task<RetrieveProfileDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var account = await _repository
                .FindByUsername(
                    _accessor.RetrieveAccountName()!,
                    cancellationToken
                );
            if (account == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            var contact = await _mediator.Send(
                new RetrieveContactInformation.Query {Id = account.ContactInformation},
                cancellationToken
            );
            if (contact == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            var address = await _mediator.Send(
                new RetrieveAddress.Query {Id = account.Address},
                cancellationToken
            );
            if (address == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            var result = new RetrieveProfileDTO
            {
                Username = account.Username,
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                Email = contact.Email,
                ContactNumber = contact.ContactNumber,
                StreetNumber = address.StreetNumber,
                StreetName = address.StreetName,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode
            };
            return result;
        }
    }
}