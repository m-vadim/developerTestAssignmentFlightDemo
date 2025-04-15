using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;

namespace FlySearch.AirwaysApi.Airways.HotAir.BookFlight;

public sealed class BookFlightCommandHandler : ICommandHandler<BookFlightCommand, string> {
	private readonly IHotAirBooking _hotAirBooking;

	public BookFlightCommandHandler(IHotAirBooking hotAirBooking) {
		_hotAirBooking = hotAirBooking;
	}

	public Task<string> Handle(BookFlightCommand command, CancellationToken calCancellationToken) {
		bool isBooked = _hotAirBooking.Book(command.FlightNumber, command.SeatNumber, command.Username);
		string response = string.Empty;
		if (isBooked) {
			response = $"HotAir: {DateTime.Now.Millisecond}"; // simulate booking unique id
		}

		return Task.FromResult(response);
	}
}
