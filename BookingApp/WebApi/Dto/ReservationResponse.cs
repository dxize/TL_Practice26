namespace WebApi.Dto;

public class ReservationResponse
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int RoomTypeId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public required string GuestName { get; set; }
    public required string GuestPhoneNumber { get; set; }
    public int Guests { get; set; }
    public decimal Total { get; set; }
    public required string Currency { get; set; }
    public bool IsCanceled { get; set; }
}
