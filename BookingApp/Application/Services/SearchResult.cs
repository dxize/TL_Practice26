using Domain.Entities;

namespace Application.Services;

public class SearchResult
{
    public Property Property { get; set; }
    public IReadOnlyList<RoomType> AvailableRoomTypes { get; set; }
}
