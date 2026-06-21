namespace WebApi.Dto;

public class RoomTypeResponse
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public required string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public required string Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int TotalRooms { get; set; }
    public required string Services { get; set; }
    public required string Amenities { get; set; }
}
