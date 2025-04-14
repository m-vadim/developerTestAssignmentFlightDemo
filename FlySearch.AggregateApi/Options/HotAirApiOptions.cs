namespace FlySearch.AggregateApi.Options;

public sealed class HotAirApiOptions : IAirlineApi {
	public required string BaseAddress { get; init; }
}
