using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Transaction;

public class TransactionRepository : ITransactionRepository
{
    private readonly Context _context;

    public TransactionRepository(Context context)
    {
        _context = context;
    }

    public async Task Save(Models.Transaction transaction, CancellationToken cancellationToken)
    {
        var sender = await _context
            .Account!
            .FirstOrDefaultAsync(
                e => e.Id == transaction.Sender,
                cancellationToken
            );
        if (sender == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.RecordNotFound
            );
        var receiver = await _context
            .Account!
            .FirstOrDefaultAsync(
                e => e.Id == transaction.Receiver,
                cancellationToken
            );
        if (receiver == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.RecordNotFound
            );
        await _context.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Transaction?> RetrieveLatestTransactionByAccountId(string? id,
        CancellationToken cancellationToken)
    {
        var record = await _context.Transaction!
            .OrderByDescending(e => e.Date)
            .ThenByDescending(e => e.Time)
            .FirstOrDefaultAsync(e => e.Sender == id, cancellationToken);
        return record;
    }
}