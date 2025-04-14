using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.Domain;

namespace FlySearch.AggregateApi.Features.FindFlight;

public sealed class FindFlightsQueryHandler : IQueryHandler<FindFlightsQuery, Flight[]> {
	private readonly IFlightAggregator _flightAggregator;

	public FindFlightsQueryHandler(IFlightAggregator flightAggregator) {
		_flightAggregator = flightAggregator;
	}

	public async Task<Flight[]> Handle(FindFlightsQuery query, CancellationToken calCancellationToken) {
		return await _flightAggregator.FindFlightAsync(query, calCancellationToken);
	}


}
