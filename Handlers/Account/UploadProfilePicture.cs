using System.Net;
using API.Exceptions;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Account;

public class UploadProfilePicture
{
    public class Command : IRequest<Unit>
    {
        public IFormFile? File { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IWebHostEnvironment _environment;

        public Handler(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.File == null)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RequiredField);
            var extensions = new[] {"image/jpg", "image/jpeg", "image/png"};
            if (!extensions.Contains(request.File.ContentType))
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.UnsupportedFileFormat);
            if (request.File.Length > 10485760)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.LargeFile);
            await using var stream = new FileStream(
                Path.Combine(_environment.ContentRootPath, $"Uploads/{Guid.NewGuid().ToString()}"),
                FileMode.Create,
                FileAccess.Write);
            await request.File?.CopyToAsync(stream, cancellationToken)!;
            return Unit.Value;
        }
    }
}