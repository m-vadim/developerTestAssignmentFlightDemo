using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.RoyalAir.Domain;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.BookFlight;

public sealed class BookFlightCommandHandler : ICommandHandler<BookFlightCommand, bool> {
	private readonly IRoyalAirBooking _royalAirBooking;

	public BookFlightCommandHandler(IRoyalAirBooking royalAirBooking) {
		_royalAirBooking = royalAirBooking;
	}

	public Task<bool> Handle(BookFlightCommand command, CancellationToken calCancellationToken) {
		return Task.FromResult(_royalAirBooking.Book(command.FlightNumber, command.SeatNumber, command.Username));
	}
}
