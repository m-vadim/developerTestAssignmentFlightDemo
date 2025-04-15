using Refit;

namespace FlySearch.AggregateApi.AirwaysClient.RoyalAir;

public interface IRoyalAirApi {
	[Get("/api/royal-air/v1/find-flight")]
	Task<FlightDescription[]> FindFlightsAsync(
		DateOnly? flightDate = null,
		string? flightNumber = null,
		string? destination = null,
		string? origin = null);

	[Post("/api/royal-air/v1/book-flight")]
	Task<string> BookFlightAsync(BookFlightRequest request);
}
