using Refit;

namespace FlySearch.ConsoleClient.Application;
public interface IFlightApi {
	[Get("/api/v1/find-flight")]
	Task<Flight[]> FindFlightsAsync(DateOnly? flightDate = null,
											   string? flightNumber = null,
											   string? destination = null,
											   string? origin = null,
											   string? sortBy = null);

	[Post("/api/v1/book-flight")]
	Task<ApiResponse<string>> BookFlightAsync(BookFlightRequest request);
}
