using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.Features.FindFlight.Domain;

namespace FlySearch.AggregateApi.Features.FindFlight;

public sealed class FindFlightsQueryHandler : IQueryHandler<FindFlightsQuery, Flight[]> {
	public FindFlightsQueryHandler(TimeProvider timeProvider) { }

	public Task<Flight[]> Handle(FindFlightsQuery query, CancellationToken calCancellationToken) {
		return Task.FromResult(Array.Empty<Flight>());
	}
}
