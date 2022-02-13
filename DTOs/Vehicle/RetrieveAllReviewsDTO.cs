namespace API.DTOs.Vehicle;

public class RetrieveAllReviewsDTO
{
    public string? Reviewer { get; set; }
    public DateTime? Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}