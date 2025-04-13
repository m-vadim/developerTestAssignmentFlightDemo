using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;

namespace FlySearch.AirwaysApi.Airways.HotAir.BookFlight;

public sealed class IsFlightBookedQueryHandler : IQueryHandler<IsFlightBookedQuery, bool> {
	private readonly IHotAirBooking _hotAirBooking;

	public IsFlightBookedQueryHandler(IHotAirBooking hotAirBooking) {
		_hotAirBooking = hotAirBooking;
	}

	public Task<bool> Handle(IsFlightBookedQuery query, CancellationToken calCancellationToken) {
		return Task.FromResult(_hotAirBooking.IsBooked(query.FlightNumber, query.SeatNumber));
	}
}
