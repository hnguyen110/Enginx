using System.Net;
using API.Exceptions;
using API.Repositories.Profile;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Profile;

public class RetrieveProfilePicture
{
    public class Query : IRequest<string?>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, string?>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IAuthorization _authorization;
        private readonly IProfilePictureRepository _repository;

        public Handler(IProfilePictureRepository repository, ICredentialAccessor accessor, IAuthorization authorization)
        {
            _repository = repository;
            _accessor = accessor;
            _authorization = authorization;
        }

        public async Task<string?> Handle(Query request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                return await _repository
                    .RetrieveProfilePictureByAccount(
                        _accessor.RetrieveAccountId(),
                        cancellationToken
                    );

            var isAdmin = await _authorization.IsAdministrator();
            if (!isAdmin)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );
            return await _repository
                .RetrieveProfilePictureByAccount(
                    request.Id,
                    cancellationToken
                );
        }
    }
}