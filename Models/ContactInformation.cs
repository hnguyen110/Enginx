namespace API.Models;

public class ContactInformation
{
    public string? Id { get; set; }
    public Account? Account { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
}