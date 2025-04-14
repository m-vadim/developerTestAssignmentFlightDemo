using FlySearch.AggregateApi.Domain;
using FlySearch.AggregateApi.Infrastructure;

namespace FlySearch.AggregateApi.Features.FindFlight;

public sealed class FlightAggregator : IFlightAggregator {
	private readonly IAirlineApi[] _airlines;

	public FlightAggregator(IAirlineApi[] airlines) {
		_airlines = airlines;
	}

	public async Task<Flight[]> FindFlightAsync(FindFlightsQuery query, CancellationToken cancellationToken = default) {
		var list = new List<Task<Flight[]>>(_airlines.Length);
		DateOnly? date = query.FlightDate.HasValue
			? DateOnly.FromDateTime(query.FlightDate.Value.Date)
			: null;

		foreach (var air in _airlines) {
			list.Add(air.FindFlightsAsync(
						 date,
						 query.FlightNumber,
						 query.Destination,
						 query.Origin,
						 query.SortBy,
						 cancellationToken));
		}

		await Task.WhenAll(list);

		var data = new List<Flight>();
		foreach(Task<Flight[]> task in list) {
			data.AddRange(task.Result);
		}

		if (!string.IsNullOrWhiteSpace(query.SortBy)) {
			return SortBy(data, query.SortBy).ToArray();
		}

		return data.ToArray();
	}

	private static IOrderedEnumerable<Flight> SortBy(IEnumerable<Flight> flights, string sortBy) {
		return sortBy switch {
			"flightDate" => flights.OrderBy(f => f.FlightDate),
			"flightNumber" => flights.OrderBy(f => f.FlightNumber),
			"destination" => flights.OrderBy(f => f.DepartureAirportCode),
			"origin" => flights.OrderBy(f => f.ArrivalAirportCode),
			_ => throw new ArgumentException($"Sort by {sortBy} is not supported")
		};
	}
}
