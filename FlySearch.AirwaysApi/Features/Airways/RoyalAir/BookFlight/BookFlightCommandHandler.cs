using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.RoyalAir.Domain;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.BookFlight;

public sealed class BookFlightCommandHandler : ICommandHandler<BookFlightCommand, string> {
	private readonly IRoyalAirBooking _royalAirBooking;

	public BookFlightCommandHandler(IRoyalAirBooking royalAirBooking) {
		_royalAirBooking = royalAirBooking;
	}

	public Task<string> Handle(BookFlightCommand command, CancellationToken calCancellationToken) {
		bool isBooked = _royalAirBooking.Book(command.FlightNumber, command.SeatNumber, command.Username);
		string response = string.Empty;
		if (isBooked) {
			response = $"RoyalAir: {DateTime.Now.Millisecond}"; // simulate booking unique id
		}

		return Task.FromResult(response);
	}
}
