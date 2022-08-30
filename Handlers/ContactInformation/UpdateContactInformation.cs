using System.Net;
using API.DTOs.Profile;
using API.Exceptions;
using API.Repositories.ContactInformation;
using API.Utilities.Messages;
using AutoMapper;
using MediatR;

namespace API.Handlers.ContactInformation;

public class UpdateContactInformation
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IContactInformationRepository _repository;

        public Handler(IContactInformationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .FindContactInformationById(
                    request.Id!,
                    cancellationToken
                );

            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            var ci = new UpdateContactInfoDTO
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                ContactNumber = request.ContactNumber
            };
            _mapper.Map(ci, record);

            await _repository.UpdateContactInformation(cancellationToken);
            return Unit.Value;
        }
    }
}