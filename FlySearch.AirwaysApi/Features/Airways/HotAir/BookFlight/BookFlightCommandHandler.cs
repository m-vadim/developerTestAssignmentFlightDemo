using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;

namespace FlySearch.AirwaysApi.Airways.HotAir.BookFlight;

public sealed class BookFlightCommandHandler : ICommandHandler<BookFlightCommand, bool> {
	private readonly IHotAirBooking _hotAirBooking;

	public BookFlightCommandHandler(IHotAirBooking hotAirBooking) {
		_hotAirBooking = hotAirBooking;
	}

	public Task<bool> Handle(BookFlightCommand command, CancellationToken calCancellationToken) {
		return Task.FromResult(_hotAirBooking.Book(command.FlightNumber, command.SeatNumber, command.Username));
	}
}
