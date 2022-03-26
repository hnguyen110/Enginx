namespace API.DTOs.Reservation;

public class RetrieveUpcomingReservationDTO
{
    public string? Id { get; set; }
    public string? Location { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public double Price { get; set; }
    public string? Insurance { get; set; }
    public string? VehicleId { get; set; }
    public string? Vehicle { get; set; }

    public List<string>? VehiclePictures { get; set; }
    public string? BodyType { get; set; }
    public string? Color { get; set; }
    public string? FuelType { get; set; }
    public string? EngineType { get; set; }
}