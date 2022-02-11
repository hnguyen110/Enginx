using System.Net;
using API.Exceptions;
using API.Repositories.Vehicle;
using API.Repositories.VehiclePicture;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Vehicle;

public class UploadVehiclePicture
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public List<IFormFile>? File { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IVehiclePictureRepository _vehiclePictureRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public Handler(
            ICredentialAccessor accessor,
            IVehiclePictureRepository vehiclePictureRepository,
            IVehicleRepository vehicleRepository
        )
        {
            _accessor = accessor;
            _vehiclePictureRepository = vehiclePictureRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository
                .RetrievedVehicleById(
                    _accessor.RetrieveAccountId(),
                    request.Id,
                    cancellationToken
                );
            if (vehicle == null)
                throw new ApiException(HttpStatusCode.NotFound, ApiErrorMessages.NotFound);
            if (string.IsNullOrEmpty(request.Id))
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            var result = await
                _vehiclePictureRepository
                    .FindVehicleById(
                        request.Id,
                        cancellationToken
                    );
            if (result == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
            foreach (var picture in request.File!)
            {
                var id = await _vehiclePictureRepository
                    .SaveVehiclePictures(
                        picture,
                        cancellationToken
                    );
                await _vehiclePictureRepository
                    .SaveToVehicle(
                        request.Id,
                        id,
                        cancellationToken
                    );
            }

            return Unit.Value;
        }
    }
}