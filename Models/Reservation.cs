namespace API.Models;

public class Reservation
{
    public string? Id { get; set; }
    public string? Date { get; set; }

    public string? Vehicle { get; set; }
    public Vehicle? VehicleReference { get; set; }
    public string? CheckInDate { get; set; }
    public string? CheckOutDate { get; set; }
    public string? CheckInTime { get; set; }

    public string? CheckOutTime { get; set; }
    // public int Insurance { get; set; }
    // public int Transaction { get; set; }
}