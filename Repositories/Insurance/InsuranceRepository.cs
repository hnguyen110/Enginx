using API.DatabaseContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Insurance;

public class InsuranceRepository : IInsuranceRepository
{
    private readonly Context _database;
    private readonly IMapper _mapper;

    public InsuranceRepository(Context database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task Save(Models.Insurance insurance, CancellationToken cancellationToken)
    {
        await _database.AddAsync(insurance, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Insurance?> RetrieveInsuranceById(string? id, CancellationToken cancellationToken)
    {
        var record = await _database
            .Insurance!
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken
            );
        return record;
    }

    public async Task<List<Models.Insurance>> RetrieveAllInsurances(CancellationToken cancellationToken)
    {
        var records = await _database
            .Insurance!
            .ToListAsync(cancellationToken);

        return records;
    }

    public async Task UpdateInsurance(Models.Insurance insurance, Models.Insurance updates,
        CancellationToken cancellationToken)
    {
        _mapper.Map(updates, insurance);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteInsurance(Models.Insurance insurance, CancellationToken cancellationToken)
    {
        _database.Remove(insurance);
        await _database.SaveChangesAsync(cancellationToken);
    }
}