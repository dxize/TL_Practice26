namespace WebApi.Dto;

public class SearchResultResponse
{
    public required PropertyResponse Property { get; set; }
    public required List<RoomTypeResponse> AvailableRoomTypes { get; set; }
}
