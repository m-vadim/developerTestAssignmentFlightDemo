using FlySearch.ConsoleClient.Application;
using Refit;

namespace FlySearch.ConsoleClient;

public sealed class Demo {
	private readonly string _baseUrl;

	public Demo(string baseUrl) {
		if (string.IsNullOrWhiteSpace(baseUrl)) {
			throw new ArgumentNullException(nameof(baseUrl));
		}

		_baseUrl = baseUrl;
	}

	public async Task RunDemoAsync() {
		var flightApi = RestService.For<IFlightApi>(_baseUrl);

		Console.WriteLine("Requesting all flights...");
		Flight[] data = await flightApi.FindFlightsAsync(null, null, null, null, null);
		RenderFlights(data);
		Console.WriteLine("Requesting all flights from LHR sorted by flightNumber");
		data = await flightApi.FindFlightsAsync(null, null, null, "LHR", "flightNumber");
		RenderFlights(data);
		Console.WriteLine("Booking flight Royal Air BA1234 seat 14A from LHR to JFK");
		ApiResponse<string> bookingResponse = await flightApi.BookFlightAsync(new BookFlightRequest {
				Airline = "Royal Air",
				FlightNumber = "BA1234",
				Username = "john.doe",
				SeatNumber = "14A"
		});
		Console.WriteLine($"Booking result: {bookingResponse.StatusCode} {bookingResponse.Content}");
		Console.WriteLine("Requesting data on flight BA1234");
		data = await flightApi.FindFlightsAsync(null, "BA1234", null, null, "flightNumber");
		Console.WriteLine("Repeat booking flight Royal Air BA1234 seat 14A from LHR to JFK");
		bookingResponse = await flightApi.BookFlightAsync(new BookFlightRequest {
			Airline = "Royal Air",
			FlightNumber = "BA1234",
			Username = "john.doe",
			SeatNumber = "14A"
		});
		Console.WriteLine($"Booking result: {bookingResponse.StatusCode} {bookingResponse.Content}");
	}

	private void RenderFlights(Flight[] flights) {
		if (flights.Length == 0) {
			Console.WriteLine("No flights found.");
			return;
		}

		foreach (var flight in flights) {
			Console.WriteLine(flight.ToString());
		}
	}
}
