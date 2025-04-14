using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;

namespace FlySearch.AirwaysApi.Airways.HotAir.FindFlight;

public sealed class FindFlightsQueryHandler : IQueryHandler<FindFlightsQuery, FlightDescription[]> {
	private readonly FlightDescription[] _data;

	public FindFlightsQueryHandler(TimeProvider timeProvider, IHotAirBooking hotAirBooking) {
		var now = timeProvider.GetLocalNow().Date;
		_data = [
			new FlightDescription(
				"BA1234",
				new Departure("LHR", now.AddHours(2)),
				new Arrival("JFK", now.AddHours(10)),
				hotAirBooking.GetAvailableSeats("BA1234").ToArray()
			),
			new FlightDescription(
				"AA4567",
				new Departure("LAX", now.AddHours(3)),
				new Arrival("ORD", now.AddHours(8)),
				hotAirBooking.GetAvailableSeats("AA4567").ToArray()
			),
			new FlightDescription(
				"DL7890",
				new Departure("ATL", now.AddHours(5)),
				new Arrival("LAS", now.AddHours(9)),
				hotAirBooking.GetAvailableSeats("DL7890").ToArray()
			),
			new FlightDescription(
				"UA2468",
				new Departure("SFO", now.AddHours(1)),
				new Arrival("DEN", now.AddHours(4)),
				hotAirBooking.GetAvailableSeats("UA2468").ToArray()
			),
			new FlightDescription(
				"LH5678",
				new Departure("FRA", now.AddHours(4)),
				new Arrival("SIN", now.AddHours(16)),
				hotAirBooking.GetAvailableSeats("LH5678").ToArray()
			),
			new FlightDescription(
				"EK3901",
				new Departure("DXB", now.AddHours(6)),
				new Arrival("BKK", now.AddHours(16)),
				hotAirBooking.GetAvailableSeats("EK3901").ToArray()
			),
			new FlightDescription(
				"SQ2187",
				new Departure("SIN", now.AddHours(5)),
				new Arrival("SYD", now.AddHours(14)),
				hotAirBooking.GetAvailableSeats("SQ2187").ToArray()
			),
			new FlightDescription(
				"QF8523",
				new Departure("SYD", now.AddHours(8)),
				new Arrival("LAX", now.AddHours(24)),
				hotAirBooking.GetAvailableSeats("QF8523").ToArray())
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
			data = data.Where(a => a.Departure.DepartureTime.Date == query.FlightDate.Value.Date);
		}

		return Task.FromResult(data.ToArray());
	}
}
