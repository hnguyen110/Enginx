using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.Vehicle;

public class CreateVehicle
{
    public class Command : IRequest<Unit>
    {
        public string? BodyType { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? EngineType { get; set; }
        public string? FuelType { get; set; }
        public string? Location { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public Single? Mileage { get; set; }
        public Single? Price { get; set; }
        public int? Year { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, ICredentialAccessor accessor)
        {
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var vehicle = new Models.Vehicle()
            {
                Id = Guid.NewGuid().ToString(),
                Account = _accessor.RetrieveAccountId(),
                BodyType = request.BodyType,
                Color = request.Color,
                Description = request.Description,
                EngineType = request.EngineType,
                FuelType = request.FuelType,
                Location = request.Location,
                Make = request.Make,
                Model = request.Model,
                Mileage = request.Mileage,
                Price = request.Price,
                Year = request.Year
                
            };
            
            await _repository.Save(vehicle, cancellationToken);
            return Unit.Value;
        }
    }
}