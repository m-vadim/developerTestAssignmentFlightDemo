using System.Collections.Concurrent;
using FlySearch.AggregateApi.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace FlySearch.AggregateApi.Infrastructure;

public sealed class CachedAirlineApi<TAirline> : IAirlineApi where TAirline : IAirlineApi {
	private readonly TAirline _airlineApi;
	private readonly IMemoryCache _cache;
	private readonly ILogger<CachedAirlineApi<TAirline>> _logger;

	public CachedAirlineApi(TAirline decoratedAirlineApi, IMemoryCache cache, ILogger<CachedAirlineApi<TAirline>> logger) {
		_airlineApi = decoratedAirlineApi;
		_cache = cache;
		_logger = logger;
	}

	public string Name => _airlineApi.Name;

	public async Task<Flight[]> FindFlightsAsync(
		DateOnly? flightDate,
		string? flightNumber,
		string? destination,
		string? origin,
		string? sortBy,
		CancellationToken cancellationToken = default) {
		var cacheKey = $"find-flights-{flightDate}-{flightNumber}-{destination}-{origin}-{sortBy}";
		if (_cache.TryGetValue(cacheKey, out Flight[] fligts))
		{
			_logger.LogInformation("Cache hit for key: {Key}", cacheKey);
			return fligts;
		}

		_logger.LogInformation("Cache miss for key: {Key}", cacheKey);
		Flight[] data = await _airlineApi.FindFlightsAsync(flightDate, flightNumber, destination, origin, sortBy, cancellationToken);
		_cache.Set(cacheKey, data, TimeSpan.FromMinutes(5));

		return data;
	}

	public async Task<BookingResult> BookAsync(string flightNumber, string seatNumber, string userName, CancellationToken cancellationToken = default) {
		BookingResult result = await _airlineApi.BookAsync(flightNumber, seatNumber, userName, cancellationToken);
		if (result.State == BookingState.Success) {
			ResetCache();
		}

		return result;
	}

	private void ResetCache() {
		if (_cache is MemoryCache concreteMemoryCache) {
			concreteMemoryCache.Clear();
		}
	}
}
