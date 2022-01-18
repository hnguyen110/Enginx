using API.DatabaseContext;
using MediatR;

namespace API.Handlers.Address;

public class CreateAddress
{
    public class Command : IRequest<string>
    {
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, string>
    {
        private readonly Context _database;

        public Handler(Context database)
        {
            _database = database;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var address = new Models.Address
            {
                StreetNumber = request.StreetNumber,
                StreetName = request.StreetName,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode,
                Id = Guid.NewGuid().ToString()
            };

            await _database.AddAsync(address, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return address.Id;
        }
    }
}