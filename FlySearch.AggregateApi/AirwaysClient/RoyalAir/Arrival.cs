namespace FlySearch.AggregateApi.AirwaysClient.RoyalAir;

public record struct Arrival(string AirportCode, DateTimeOffset ArrivalTime);
