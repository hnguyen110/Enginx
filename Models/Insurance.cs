namespace API.Models;

public class Insurance
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }

    public List<Reservation>? Reservations { get; set; }
}