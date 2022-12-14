using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.VehiclePicture;

public class VehiclePictureRepository : IVehiclePictureRepository
{
    private readonly Context _context;
    private readonly IWebHostEnvironment _environment;

    public VehiclePictureRepository(IWebHostEnvironment environment, Context context)
    {
        _environment = environment;
        _context = context;
    }

    public async Task<string> SaveVehiclePictures(IFormFile file, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid().ToString();
        await using var stream = new FileStream(
            Path.Combine(_environment.ContentRootPath, $"Uploads/{id}"),
            FileMode.Create,
            FileAccess.Write);
        await file.CopyToAsync(stream, cancellationToken);
        return id;
    }

    public async Task SaveToVehicle(string? vehicle, string id, CancellationToken cancellationToken)
    {
        var result = await
            _context
                .Vehicle!
                .FirstOrDefaultAsync(
                    e =>
                        e.Id == vehicle, cancellationToken
                );
        if (result == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.NotFound
            );
        var picture = new Models.VehiclePicture
        {
            Id = Guid.NewGuid().ToString(),
            FilePath = Path.Combine(_environment.ContentRootPath, $"Uploads/{id}"),
            Vehicle = vehicle
        };
        await _context.AddAsync(picture, cancellationToken);
        result.VehiclePictures!.Add(picture);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<string>> RetrieveVehiclePicturesById(string? id,
        CancellationToken cancellationToken)
    {
        var records = await
            _context
                .VehiclePicture!
                .Where(e => e.Vehicle == id)
                .Select(e => Convert.ToBase64String(File.ReadAllBytes(e.FilePath!)))
                .ToListAsync(cancellationToken);
        return records;
    }
}