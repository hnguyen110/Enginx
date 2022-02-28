using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Vehicle;

public class VehicleRepository : IVehicleRepository
{
    private readonly Context _database;

    public VehicleRepository(Context database)
    {
        _database = database;
    }

    public async Task Save(Models.Vehicle? vehicle, CancellationToken cancellationToken)
    {
        if (vehicle is { })
            await _database.AddAsync(vehicle, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Vehicle?> RetrieveVehicleById(string? id, CancellationToken cancellationToken)
    {
        var record = await _database
            .Vehicle!
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        return record;
    }

    public async Task<Models.Vehicle?> RetrieveVehicleById(string? owner, string? id,
        CancellationToken cancellationToken)
    {
        var record = await _database
            .Vehicle!
            .FirstOrDefaultAsync(
                e => e.Owner == owner
                     && e.Id == id,
                cancellationToken
            );
        return record;
    }

    public async Task<Models.Vehicle?> RetrievePublishedVehicleById(string? id, CancellationToken cancellationToken)
    {
        var record = await _database
            .Vehicle!
            .FirstOrDefaultAsync(
                e => e.Id == id
                     && e.Approved == true
                     && e.Published == true,
                cancellationToken
            );
        return record;
    }

    public async Task<List<Models.Vehicle>> RetrieveAllVehiclesByOwnerId(string? owner,
        CancellationToken cancellationToken)
    {
        var records = await _database
            .Vehicle!
            .Where(e => e.Owner == owner)
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task<List<Models.Vehicle>> RetrieveVehiclesByLocation(string? location,
        CancellationToken cancellationToken)
    {
        var records = await _database
            .Vehicle!
            .Where(e => e.Location!.ToLower().Trim() == location!.ToLower().Trim())
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task<List<Models.Review>> RetrieveAllVehicleReviews(string? id, CancellationToken cancellationToken)
    {
        var vehicle = await _database
            .Vehicle!
            .FirstOrDefaultAsync(
                e =>
                    e.Id == id
                    && e.Approved == true
                    && e.Published == true,
                cancellationToken
            );
        if (vehicle == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.RecordNotFound
            );
        var records = await _database
            .Review!
            .Where(e => e.Vehicle == id)
            .Include(e => e.ReviewerReference)
            .ThenInclude(e => e!.ContactInformationReference)
            .OrderByDescending(e => e.Date)
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task<List<Models.Vehicle>> RetrieveAllPublishedVehicles(CancellationToken cancellationToken)
    {
        var records = await _database
            .Vehicle!
            .Where(e => e.Published == true && e.Approved == true)
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task<List<Models.Vehicle>> RetrieveAllVehicles(CancellationToken cancellationToken)
    {
        var records = await _database
            .Vehicle!
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task ApproveVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken)
    {
        if (!vehicle.Approved)
        {
            vehicle.Approved = true;
            await _database.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RejectVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken)
    {
        if (vehicle.Approved)
        {
            vehicle.Approved = false;
            await _database.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken)
    {
        var parent = await _database.Vehicle!
            .Include(e => e.Reservations)
            .Include(e => e.VehiclePictures)
            .Include(e => e.Reviews)
            .FirstOrDefaultAsync(e => e.Id == vehicle.Id, cancellationToken);

        _database.Remove(parent!);
        await _database.SaveChangesAsync(cancellationToken);
    }
}