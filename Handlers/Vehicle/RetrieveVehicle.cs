using System.Net;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveVehicle
{
    public class Query : IRequest<RetrieveVehicleDTO>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, RetrieveVehicleDTO>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IVehicleRepository _repository;

        public Handler(ICredentialAccessor accessor, IVehicleRepository repository, IMapper mapper, IMediator mediator)
        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<RetrieveVehicleDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .RetrieveVehicleById(
                    _accessor.RetrieveAccountId(), request.Id,
                    cancellationToken
                );

            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            var pictures = await _mediator.Send(
                new RetrieveVehiclePictures.Query { Id = request.Id },
                cancellationToken
            );

            var vehicle = new RetrieveVehicleDTO
            {
                Id = record.Id,
                BodyType = record.BodyType,
                Color = record.Color,
                Description = record.Description,
                EngineType = record.EngineType,
                FuelType = record.FuelType,
                Location = record.Location,
                Make = record.Make,
                Model = record.Model,
                Mileage = record.Mileage,
                Price = record.Price,
                Year = record.Year,
                Pictures = pictures,
                RegistrationDate = record.RegistrationDate,
                TransmissionType = record.TransmissionType
            };

            return vehicle;
        }
    }
}