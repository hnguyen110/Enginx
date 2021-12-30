using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.ContactInformation;

public class CreateContactInformation
{
    public class Command : IRequest<string>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
    }

    public class Handler : IRequestHandler<Command, string>
    {
        private readonly Context _database;

        public Handler(Context database)
        {
            _database = database;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _database
                .ContactInformation
                !.FirstOrDefaultAsync(e =>
                        e.Email == request.Email
                        || e.ContactNumber == request.ContactNumber,
                    cancellationToken);

            if (record != null)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);

            var contact = new Models.ContactInformation
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request!.FirstName,
                LastName = request!.LastName,
                MiddleName = request.MiddleName,
                Email = request!.Email,
                ContactNumber = request!.ContactNumber
            };

            await _database.AddAsync(contact, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return contact.Id;
        }
    }
}