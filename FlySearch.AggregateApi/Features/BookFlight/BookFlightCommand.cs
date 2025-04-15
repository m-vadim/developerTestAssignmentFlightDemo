namespace FlySearch.AggregateApi.Features.BookFlight;

public record BookFlightCommand(string FlightNumber, string SeatNumber, string Username, string Airline);
