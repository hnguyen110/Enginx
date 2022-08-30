namespace API.DTOs.Insurance;

public class CreateInsuranceDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}