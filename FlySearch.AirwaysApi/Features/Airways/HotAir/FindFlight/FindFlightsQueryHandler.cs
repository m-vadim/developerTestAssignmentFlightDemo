using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;

namespace FlySearch.AirwaysApi.Airways.HotAir.FindFlight;

public sealed class FindFlightsQueryHandler : IQueryHandler<FindFlightsQuery, FlightDescription[]> {
	private readonly FlightDescription[] _data;

	public FindFlightsQueryHandler(TimeProvider timeProvider) {
		DateTime now = timeProvider.GetLocalNow().Date;

		_data = [
			new FlightDescription(
				"BA1234",
				new Departure("LHR", now.AddHours(2)),
				new Arrival("JFK", now.AddHours(10)),
				[
					new Seat("Economy", false, "14A", 100),
					new Seat("Economy", false, "14B", 100),
					new Seat("Economy", true, "25C", 100),
					new Seat("Business", true, "2A", 300)
				]
			),
			new FlightDescription(
				"AA4567",
				new Departure("LAX", now.AddHours(3)),
				new Arrival("ORD", now.AddHours(8)),
				[
					new Seat("Economy", false, "18D", 200),
					new Seat("Economy", true, "23F", 200),
					new Seat("First", true, "1A", 1000)
				]
			),
			new FlightDescription(
				"DL7890",
				new Departure("ATL", now.AddHours(5)),
				new Arrival("LAS", now.AddHours(9)),
				[
					new Seat("Economy", false, "32B", 300),
					new Seat("Economy", false, "32C", 300),
					new Seat("Comfort+", true, "12D", 400)
				]
			),
			new FlightDescription(
				"UA2468",
				new Departure("SFO", now.AddHours(1)),
				new Arrival("DEN", now.AddHours(4)),
				[
					new Seat("Economy", false, "19A", 100),
					new Seat("Economy", false, "19B", 100),
					new Seat("Business", true, "5C", 300)
				]
			),
			new FlightDescription(
				"LH5678",
				new Departure("FRA", now.AddHours(4)),
				new Arrival("SIN", now.AddHours(16)),
				[
					new Seat("Economy", false, "44E", 100),
					new Seat("Premium Economy", true, "27K", 140),
					new Seat("Business", true, "8D", 300),
					new Seat("First", true, "2A", 600)
				]
			),
			new FlightDescription(
				"EK3901",
				new Departure("DXB", now.AddHours(6)),
				new Arrival("BKK", now.AddHours(16)),
				[
					new Seat("Economy", false, "51G", 100),
					new Seat("Business", true, "11K", 200),
					new Seat("First", true, "1F", 500)
				]
			),
			new FlightDescription(
				"SQ2187",
				new Departure("SIN", now.AddHours(5)),
				new Arrival("SYD", now.AddHours(14)),
				[
					new Seat("Economy", false, "38A", 100),
					new Seat("Economy", false, "38B", 100),
					new Seat("Premium Economy", true, "22D", 200),
					new Seat("Business", true, "15K", 400)
				]
			),
			new FlightDescription(
				"QF8523",
				new Departure("SYD", now.AddHours(8)),
				new Arrival("LAX", now.AddHours(24)),
				[
					new Seat("Economy", false, "47C", 100),
					new Seat("Premium Economy", true, "31A", 200),
					new Seat("Business", true, "9J", 500),
					new Seat("First", true, "3F", 900)
				]
			)
		];
	}

	public Task<FlightDescription[]> Handle(FindFlightsQuery query, CancellationToken calCancellationToken) {
		IEnumerable<FlightDescription> data = _data.Select(a => a);

		if (!string.IsNullOrEmpty(query.FlightNumber)) {
			data = data.Where(a => a.FlightNumber == query.FlightNumber);
		}

		if (!string.IsNullOrEmpty(query.Origin)) {
			data = data.Where(a => a.Departure.AirportCode == query.Origin);
		}

		if (!string.IsNullOrEmpty(query.Destination)) {
			data = data.Where(a => a.Arrival.AirportCode == query.Destination);
		}

		if (query.FlightDate.HasValue) {
			data = data.Where(a => a.Departure.DepartureTime.Date == query.FlightDate);
		}

		return Task.FromResult(data.ToArray());
	}
}
