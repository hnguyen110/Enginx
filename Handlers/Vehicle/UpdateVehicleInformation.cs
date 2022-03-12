using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class UpdateVehicleInformation
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
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

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public Handler(ICredentialAccessor accessor, IMapper mapper, IVehicleRepository repository)
        {
            _accessor = accessor;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record =
                await _repository
                    .RetrieveVehicleById(
                        _accessor.RetrieveAccountId(),
                        request.Id,
                        cancellationToken
                    );
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            _mapper.Map(request, record);
            await _repository.Save(null, cancellationToken);
            return Unit.Value;
        }
    }
}