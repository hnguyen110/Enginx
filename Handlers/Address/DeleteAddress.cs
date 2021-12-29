using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.CredentialAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Address;

public class DeleteAddress
{
    public class Command : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly Context _database;

        public Handler(Context database, ICredentialAccessor accessor)
        {
            _database = database;
            _accessor = accessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record =
                await _database.Address!.FirstOrDefaultAsync(e => e.Id == request.Id,
                    cancellationToken);
            if (record == null) throw new ApiException(HttpStatusCode.NotFound, "Note not found");

            if (record.Id != _accessor.RetrieveAccountId())
                throw new ApiException(HttpStatusCode.Unauthorized, "Access denied to note");

            _database.Remove(record);
            await _database.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}