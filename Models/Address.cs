namespace API.Models;

public class Address
{
    public string? Id { get; set; }
    public Account? Account { get; set; }
    public int StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
}