namespace API.Models;

public class Vehicle
{
    public string? Id { get; set; }
    public string? Owner { get; set; }
    public bool Published { get; set; } = false;
    public bool Approved { get; set; } = false;
    public Account? OwnerReference { get; set; }
    public string? BodyType { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public string? EngineType { get; set; }
    public string? TransmissionType { get; set; }
    public string? FuelType { get; set; }
    public string? Location { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public double Mileage { get; set; }
    public double Price { get; set; }
    public int Year { get; set; }
    public DateTime? RegistrationDate { get; set; } = DateTime.Now;
    public List<VehiclePicture>? VehiclePictures { get; set; }
    public List<Reservation>? Reservations { get; set; }
    public List<Review>? Reviews { get; set; }
}