using FlySearch.AggregateApi.Domain;

namespace FlySearch.AggregateApi.Infrastructure;

public interface IAirlineApi {
	string Name { get; }
	Task<Flight[]> FindFlightsAsync(DateOnly? flightDate,
									string? flightNumber,
									string? destination,
									string? origin,
									string? sortBy,
									CancellationToken cancellationToken = default);
}
