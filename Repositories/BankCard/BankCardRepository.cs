using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.BankCard;

public class BankCardRepository : IBankCardRepository
{
    private readonly Context _database;

    public BankCardRepository(Context database)
    {
        _database = database;
    }

    public async Task Save(Models.BankCard bankCard, CancellationToken cancellationToken)
    {
        var record =
            await _database.BankCard!.FirstOrDefaultAsync(e => e.CardNumber == bankCard.CardNumber, cancellationToken);

        if (record != null)
            throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);

        await _database.AddAsync(bankCard, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }
}