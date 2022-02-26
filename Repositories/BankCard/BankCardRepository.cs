using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.BankCard;

public class BankCardRepository : IBankCardRepository
{
    private readonly Context _database;
    private readonly IMapper _mapper;

    public BankCardRepository(Context database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
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

    public async Task<Models.BankCard?> RetrieveBankCardById(string? id, CancellationToken cancellationToken)
    {
        var record =
            await _database
                .BankCard!
                .FirstOrDefaultAsync(
                    e => e.Id == id,
                    cancellationToken
                );
        return record;
    }

    public async Task<List<Models.BankCard>> RetrieveAllBankCardsByAccount(string? account,
        CancellationToken cancellationToken)
    {
        var records = await _database
            .BankCard!
            .Where(e => e.Account == account)
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task UpdateBankCard(Models.BankCard bankCard, Models.BankCard updates,
        CancellationToken cancellationToken)
    {
        _mapper.Map(updates, bankCard);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBankCard(Models.BankCard? bankcard, CancellationToken cancellationToken)
    {
        _database.Remove(bankcard!);
        await _database.SaveChangesAsync(cancellationToken);
    }
}