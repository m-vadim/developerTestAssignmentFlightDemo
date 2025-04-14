namespace FlySearch.AggregateApi.AirwaysClient.HotAir;

public record FlightDescription(string FlightNumber, Departure Departure, Arrival Arrival, HotAirSeat[] SeatsAvailable, DateOnly FlightDate);
