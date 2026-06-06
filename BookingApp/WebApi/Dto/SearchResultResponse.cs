namespace WebApi.Dto;

public class SearchResultResponse
{
    public PropertyResponse Property { get; set; }
    public List<RoomTypeResponse> AvailableRoomTypes { get; set; }
}
