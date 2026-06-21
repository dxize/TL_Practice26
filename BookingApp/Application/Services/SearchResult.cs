using Domain.Entities;

namespace Application.Services;

public class SearchResult
{
    public required Property Property { get; set; }
    public required IReadOnlyList<RoomType> AvailableRoomTypes { get; set; }
}
