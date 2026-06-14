using Domain.Entities;

namespace Domain.Services;

public interface ISearchService
{
    IReadOnlyList<SearchResult> Search(
        string city,
        DateTime arrivalDate,
        DateTime departureDate,
        int guests,
        decimal? maxPrice );
}

public class SearchResult
{
    public Property Property { get; set; }

    public List<RoomType> AvailableRoomTypes { get; set; } = new();
}
