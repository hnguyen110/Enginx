using API.Repositories.Account;
using API.Utilities.CredentialAccessor;
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
        private readonly ICredentialAccessor _accessor;
        private readonly IProfilePictureRepository _profilePictureRepository;

        public Handler(ICredentialAccessor accessor, IProfilePictureRepository profilePictureRepository)
        {
            _accessor = accessor;
            _profilePictureRepository = profilePictureRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var id = await _profilePictureRepository
                .SaveProfilePicture(
                    request.File!,
                    cancellationToken
                );
            await _profilePictureRepository
                .SaveToAccount(
                    _accessor.RetrieveAccountId(),
                    id,
                    cancellationToken
                );
            return Unit.Value;
        }
    }
}