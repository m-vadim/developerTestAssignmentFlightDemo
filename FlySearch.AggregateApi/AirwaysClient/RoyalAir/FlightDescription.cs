namespace FlySearch.AggregateApi.AirwaysClient.RoyalAir;

public record FlightDescription(string FlightNumber, Departure Departure, Arrival Arrival, RoyalAirSeat[] SeatsAvailable, DateOnly FlightDate);
