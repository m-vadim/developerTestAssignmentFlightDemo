namespace FlySearch.AggregateApi.AirwaysClient.HotAir;

public record struct Departure(string AirportCode, DateTimeOffset DepartureTime);
