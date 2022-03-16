namespace API.DTOs.Review;

public class CreateVehicleReviewDTO
{
    public string? Id { get; set; }
    public string? Reviewer { get; set; }
    public string? Vehicle { get; set; }
    public DateTime? Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}