using MediatR;

namespace API.Handlers.Address;

public class UpdateAddress
{
    public class Command : IRequest<Unit>
    {
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

    }
    
    // public class Handler : 

}