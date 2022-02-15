using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Repositories.ContactInformation;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.ContactInformation;

public class UpdateContactInformation
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IContactInformationRepository _repository;

        public Handler(IContactInformationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .FindContactInformationById(
                    request.Id!,
                    cancellationToken
                );
                
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound, 
                    ApiErrorMessages.NotFound
                );
            
            record.FirstName = request!.FirstName;
            record.LastName = request!.LastName;
            record.MiddleName = request.MiddleName;
            record.Email = request!.Email;
            record.ContactNumber = request!.ContactNumber;

            await _repository.UpdateContactInformation(cancellationToken);
            return Unit.Value;
        }
    }
}