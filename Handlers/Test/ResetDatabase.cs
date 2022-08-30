using API.DatabaseContext;
using MediatR;

namespace API.Handlers.Test;

public class ResetDatabase
{
    public class Command : IRequest<Unit>
    {
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly Context _context;

        public Handler(Context context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Account!.RemoveRange(_context.Account);
            _context.Address!.RemoveRange(_context.Address);
            _context.BankCard!.RemoveRange(_context.BankCard);
            _context.ContactInformation!.RemoveRange(_context.ContactInformation);
            _context.Insurance!.RemoveRange(_context.Insurance);
            _context.License!.RemoveRange(_context.License);
            _context.ProfilePicture!.RemoveRange(_context.ProfilePicture);
            _context.Reservation!.RemoveRange(_context.Reservation);
            _context.Review!.RemoveRange(_context.Review);
            _context.Transaction!.RemoveRange(_context.Transaction);
            _context.Vehicle!.RemoveRange(_context.Vehicle);
            _context.VehiclePicture!.RemoveRange(_context.VehiclePicture);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}