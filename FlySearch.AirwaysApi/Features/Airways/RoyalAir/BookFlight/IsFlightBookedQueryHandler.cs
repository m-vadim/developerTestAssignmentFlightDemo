using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.RoyalAir.Domain;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.BookFlight;

public sealed class IsFlightBookedQueryHandler : IQueryHandler<IsFlightBookedQuery, bool> {
	private readonly IRoyalAirBooking _royalAirBooking;

	public IsFlightBookedQueryHandler(IRoyalAirBooking hotAirBooking) {
		_royalAirBooking = hotAirBooking;
	}

	public Task<bool> Handle(IsFlightBookedQuery query, CancellationToken calCancellationToken) {
		return Task.FromResult(_royalAirBooking.IsBooked(query.FlightNumber, query.SeatNumber));
	}
}
