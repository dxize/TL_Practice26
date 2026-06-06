namespace WebApi.Dto;

public class ModifyRoomTypeRequest
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int TotalRooms { get; set; }
    public string Services { get; set; }
    public string Amenities { get; set; }
}
