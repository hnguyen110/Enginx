using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.CredentialAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Reservation;

public class DeleteReservation
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
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
                await _database.Reservation!.FirstOrDefaultAsync(e => e.Id == request.Id,
                    cancellationToken);
            if (record == null) throw new ApiException(HttpStatusCode.NotFound, "Reservation not found");

            _database.Remove(record);
            await _database.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}