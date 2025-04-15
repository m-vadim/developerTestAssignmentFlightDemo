using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.Domain;
using FlySearch.AggregateApi.Infrastructure;

namespace FlySearch.AggregateApi.Features.BookFlight;

public sealed class BookFlightCommandHandler : ICommandHandler<BookFlightCommand, BookingResult> {
	private readonly IAirlineApi[] _airlines;

	public BookFlightCommandHandler(IAirlineApi[] airlines) {
		_airlines = airlines;
	}

	public async Task<BookingResult> Handle(BookFlightCommand command, CancellationToken cancellationToken) {
		var airline = _airlines.Single(a => a.Name == command.Airline);
		return await airline.BookAsync(command.FlightNumber, command.SeatNumber, command.Username, cancellationToken);
	}
}
