namespace WebApi.Dto;

public class ModifyPropertyRequest
{
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
