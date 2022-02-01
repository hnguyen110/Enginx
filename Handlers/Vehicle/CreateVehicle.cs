using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.Vehicle;

public class CreateVehicle
{
    public class Command : IRequest<Unit>
    {
        public string? Location { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? BodyType { get; set; }
        public string? EngineType { get; set; }
        public string? TransmissionType { get; set; }
        public string? FuelType { get; set; }
        public int? Mileage { get; set; }
        public int? NumOfSeats { get; set; }
        public int? MaxNumOfSeats { get; set; }
        public int? RentalPrice { get; set; }   
        public string? Description { get; set; }
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
                Location = request.Location,
                Make = request.Make,
                Model = request.Model,
                BodyType = request.BodyType,
                EngineType = request.EngineType,
                TransmissionType = request.TransmissionType,
                FuelType = request.FuelType,
                Mileage = request.Mileage,
                NumOfSeats = request.NumOfSeats,
                MaxNumOfSeats = request.MaxNumOfSeats,
                RentalPrice = request.RentalPrice,
                Description = request.Description
            };
            
            await _repository.Save(vehicle, cancellationToken);
            return Unit.Value;
        }
    }
}