using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.CredentialAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Vehicle;

public class UpdateVehicle
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public string? BodyType { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? EngineType { get; set; }
        public string? FuelType { get; set; }
        public string? Location { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public double Mileage { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
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
                await _database.Vehicle!.FirstOrDefaultAsync(e => e.Id == request.Id,
                    cancellationToken);
            if (record == null) throw new ApiException(HttpStatusCode.NotFound, "Vehicle not found");

            if (record.Owner != _accessor.RetrieveAccountId())
                throw new ApiException(HttpStatusCode.Unauthorized, "Access denied to vehicle");

            record.Description = request.Description;
            record.BodyType = request.BodyType;
            record.Color = request.Color;
            record.EngineType = request.EngineType;
            record.Location = request.Location;
            record.Make = request.Make;
            record.Mileage = request.Mileage;
            record.FuelType = request.FuelType;
            record.Price = request.Price;
            record.Model = request.Model;
            record.Year = request.Year;
            
            await _database.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}