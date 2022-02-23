using API.Models;

namespace API.DTOs.Account;

public class RetrieveAllClientAccountDTO
{
    public string? Id { get; set; }
    public bool Approved { get; set; }
    public Role Role { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public int StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
}