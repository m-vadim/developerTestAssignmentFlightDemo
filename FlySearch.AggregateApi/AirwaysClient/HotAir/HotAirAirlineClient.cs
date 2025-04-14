using FlySearch.AggregateApi.Domain;
using FlySearch.AggregateApi.Infrastructure;

namespace FlySearch.AggregateApi.AirwaysClient.HotAir;

public sealed class HotAirAirlineClient : IAirlineApi {
	private readonly IHotAirApi _hotAirApi;
	private readonly ILogger _logger;

	public HotAirAirlineClient(IHotAirApi hotAirApi, ILogger<HotAirAirlineClient> logger) {
		_hotAirApi = hotAirApi;
		_logger = logger;
	}

	public string Name => "Hot Air";
	public async Task<Flight[]> FindFlightsAsync(DateOnly? flightDate,
												 string? flightNumber,
												 string? destination,
												 string? origin,
												 string? sortBy,
												 CancellationToken cancellationToken = default) {
		try {
			FlightDescription[] data = await _hotAirApi.FindFlightsAsync(flightDate,
																		 flightNumber,
																		 destination,
																		 origin);
			if (data is { Length: > 0 }) {
				return data.Select(Map).ToArray();
			}
		} catch (Exception e) {
			_logger.LogError("Failed to get flights from {Name}: {Message}", Name, e.Message);
		}

		return [];
	}

	private Flight Map(FlightDescription fd) {
		return new Flight {
			Airline = Name,
			FlightNumber = fd.FlightNumber,
			ArrivalAirportCode = fd.Arrival.AirportCode,
			ArrivalTime = fd.Arrival.ArrivalTime,
			DepartureAirportCode = fd.Departure.AirportCode,
			DepartureTime = fd.Departure.DepartureTime,
			Seats = fd.SeatsAvailable.Select(a => new Seat(a.Number, a.Class, a.ExtraSpace, a.Price)).ToArray(),
			FlightDate = DateOnly.FromDateTime(fd.Departure.DepartureTime.Date)
		};
	}
}
