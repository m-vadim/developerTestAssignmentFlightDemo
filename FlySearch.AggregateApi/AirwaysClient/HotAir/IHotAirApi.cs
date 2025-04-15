using Refit;

namespace FlySearch.AggregateApi.AirwaysClient.HotAir;

public interface IHotAirApi {
	[Get("/api/hot-air/v1/find-flight")]
	Task<FlightDescription[]> FindFlightsAsync(DateOnly? flightDate = null,
											   string? flightNumber = null,
											   string? destination = null,
											   string? origin = null);

	[Post("/api/hot-air/v1/book-flight")]
	Task<string> BookFlightAsync(BookFlightRequest request);
}
