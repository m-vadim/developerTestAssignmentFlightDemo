namespace FlySearch.AggregateApi.AirwaysClient.HotAir;

public record struct Arrival(string AirportCode, DateTimeOffset ArrivalTime);
