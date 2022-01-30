using System.Net;
using API.Exceptions;
using API.Repositories.ContactInformation;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.ContactInformation;

public class RetrieveContactInformation
{
    public class Query : IRequest<Models.ContactInformation>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Models.ContactInformation>
    {
        private readonly IContactInformationRepository _repository;

        public Handler(IContactInformationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Models.ContactInformation> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository.FindContactInformationById(request.Id!, cancellationToken);
            if (record == null)
                throw new ApiException(HttpStatusCode.NotFound, ApiErrorMessages.NotFound);
            return record;
        }
    }
}