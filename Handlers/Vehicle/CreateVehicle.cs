using API.DTOs.Vehicle;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class CreateVehicle
{
    public class Command : IRequest<RetrieveVehicleDTO>
    {
        public string? BodyType { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? EngineType { get; set; }
        public string? FuelType { get; set; }
        public string? TransmissionType { get; set; }
        public string? Location { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public double Mileage { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
    }

    public class Handler : IRequestHandler<Command, RetrieveVehicleDTO>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, ICredentialAccessor accessor, IMapper mapper)
        {
            _repository = repository;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<RetrieveVehicleDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var vehicle = new Models.Vehicle
            {
                Id = Guid.NewGuid().ToString(),
                Owner = _accessor.RetrieveAccountId(),
                BodyType = request.BodyType,
                Color = request.Color,
                Description = request.Description,
                EngineType = request.EngineType,
                FuelType = request.FuelType,
                TransmissionType = request.TransmissionType,
                Location = request.Location,
                Make = request.Make,
                Model = request.Model,
                Mileage = request.Mileage,
                Price = request.Price,
                Year = request.Year
            };

            await _repository.Save(vehicle, cancellationToken);
            return _mapper.Map<Models.Vehicle, RetrieveVehicleDTO>(vehicle);
        }
    }
}