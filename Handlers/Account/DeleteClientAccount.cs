using System.Net;
using API.Exceptions;
using API.Repositories.Account;
using API.Repositories.Address;
using API.Repositories.ContactInformation;
using API.Repositories.License;
using API.Repositories.Profile;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Account;

public class DeleteClientAccount
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAuthorization _authorization;
        private readonly IAccountRepository _account;
        private readonly IContactInformationRepository _contact;
        private readonly IAddressRepository _address;
        private readonly ILicenseRepository _license;
        private readonly IProfilePictureRepository _profilePicture;

        public Handler(
            IAuthorization authorization,
            IAccountRepository account,
            IContactInformationRepository contact,
            IAddressRepository address,
            ILicenseRepository license,
            IProfilePictureRepository profilePicture
        )
        {
            _authorization = authorization;
            _account = account;
            _contact = contact;
            _address = address;
            _license = license;
            _profilePicture = profilePicture;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var account = await _account.FindById(request.Id!, cancellationToken);
            if (account == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _contact.DeleteContactInformation(account.ContactInformationReference!, cancellationToken);
            await _address.DeleteAddress(account.AddressReference!, cancellationToken);
            if (account.LicenseReference != null)
                await _license
                    .DeleteLicense(
                        account.LicenseReference,
                        cancellationToken
                    );
            if (account.ProfilePictureReference != null)
                await _profilePicture
                    .DeleteProfilePicture(
                        account.ProfilePictureReference,
                        cancellationToken
                    );
            await _account.DeleteClientAccount(account, cancellationToken);
            return Unit.Value;
        }
    }
}