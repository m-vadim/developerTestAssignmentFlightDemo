using FlySearch.AggregateApi.Domain;

namespace FlySearch.AggregateApi.Features.FindFlight;

public interface IFlightAggregator {
	Task<Flight[]> FindFlightAsync(FindFlightsQuery query, CancellationToken cancellationToken = default);
}
