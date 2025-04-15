using System.Diagnostics.CodeAnalysis;
using FlySearch.AggregateApi.Domain;

namespace FlySearch.AggregateApi.Infrastructure;

public interface IAirlineApi {
	string Name { get; }

	[return: NotNull]
	Task<Flight[]> FindFlightsAsync(DateOnly? flightDate,
									string? flightNumber,
									string? destination,
									string? origin,
									string? sortBy,
									CancellationToken cancellationToken = default);
	Task<BookingResult> BookAsync(string flightNumber,
									string seatNumber,
									string userName,
									CancellationToken cancellationToken = default);
}
