namespace FlySearch.AggregateApi.AirwaysClient.RoyalAir;

public record struct Departure(string AirportCode, DateTimeOffset DepartureTime);
