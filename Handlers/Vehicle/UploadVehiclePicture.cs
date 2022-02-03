using System.Net;
using API.Exceptions;
using API.Repositories.VehiclePicture;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Vehicle;

public class UploadVehiclePicture
{
    public class Command : IRequest<Unit>
    {
        public List<IFormFile>? File { get; set; }
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command,Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IVehiclePictureRepository _vehiclePictureRepository;

        public Handler(ICredentialAccessor accessor, IVehiclePictureRepository vehiclePictureRepository)
        {
            _accessor = accessor;
            _vehiclePictureRepository = vehiclePictureRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = _vehiclePictureRepository.FindVehicleById(request.Id, cancellationToken);
            if (result == null)
                throw new ApiException(HttpStatusCode.NotFound, ApiErrorMessages.NotFound);
            foreach (var pic in request.File)
            {
                var id = await _vehiclePictureRepository
                    .SaveVehiclePictures(
                        pic,
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