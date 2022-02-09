namespace API.Models;

public class Review
{
    public string? Id { get; set; }
    public string? Reviewer { get; set; }
    public Account? ReviewerReference { get; set; }
    public string? Vehicle { get; set; }
    public Vehicle? VehicleReference { get; set; }
    public DateTime? Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}