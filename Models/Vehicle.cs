namespace API.Models;

public class Vehicle
{
    public string? Id { get; set; }
    public string? Account { get; set; }
    public Account? AccountReference { get; set; }
    public string? BodyType { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public string? EngineType { get; set; }
    public string? FuelType { get; set; }
    public string? Location { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public Single? Mileage { get; set; }
    public Single? Price { get; set; }
    public int? Year { get; set; }



}