namespace FlySearch.AggregateApi.Options;

public sealed class RoyalAirApiOptions : IAirlineApi {
	public required string BaseAddress { get; init; }
}
