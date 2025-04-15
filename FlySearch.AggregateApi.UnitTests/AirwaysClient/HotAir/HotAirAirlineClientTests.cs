using System.Net;
using FluentAssertions;
using FlySearch.AggregateApi.AirwaysClient.HotAir;
using FlySearch.AggregateApi.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FlySearch.AggregateApi.UnitTests.AirwaysClient.HotAir;

[TestFixture]
[Parallelizable]
internal sealed class HotAirAirlineClientTests {
	private Mock<IHotAirApi> _hotAirApiMock;
	private Mock<ILogger<HotAirAirlineClient>> _loggerMock;
	private HotAirAirlineClient _client;

	private static readonly DateTime Now = new(2000, 1, 1);

	[SetUp]
	public void SetUp() {
		_hotAirApiMock = new Mock<IHotAirApi>();
		_loggerMock = new Mock<ILogger<HotAirAirlineClient>>();
		_client = new HotAirAirlineClient(_hotAirApiMock.Object, _loggerMock.Object);
	}

	[Test]
	public async Task ReturnsMappedFlightResults() {
		var fd = new FlightDescription(
			"123",
			new Departure("LAX", Now),
			new Arrival("JFK", Now),
			[new HotAirSeat("1A", "Economy", true, 100)],
			DateOnly.FromDateTime(Now)
		);
		_hotAirApiMock.Setup(api => api.FindFlightsAsync(It.IsAny<DateOnly?>(),
														 It.IsAny<string?>(),
														 It.IsAny<string?>(),
														 It.IsAny<string?>()))
			.ReturnsAsync([fd]);

		Flight[] result = await _client.FindFlightsAsync(null, null, null, null, null);

		result.Should().BeEquivalentTo([new Flight {
			Airline = "Hot Air",
			FlightNumber = fd.FlightNumber,
			ArrivalAirportCode = fd.Arrival.AirportCode,
			ArrivalTime = fd.Arrival.ArrivalTime,
			DepartureAirportCode = fd.Departure.AirportCode,
			DepartureTime = fd.Departure.DepartureTime,
			Seats = fd.SeatsAvailable.Select(a => new Seat(a.Number, a.Class, a.ExtraSpace, a.Price)).ToArray(),
			FlightDate = DateOnly.FromDateTime(fd.Departure.DepartureTime.Date)
		}]);
	}

	[Test]
	public async Task ReturnsEmptyArrayWhenAirlineClineThrowsException() {
		_hotAirApiMock.Setup(api => api.FindFlightsAsync(It.IsAny<DateOnly?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
			.ThrowsAsync(new Exception("API error"));

		Flight[] result = await _client.FindFlightsAsync(null, null, null, null, null);

		result.Should().BeEmpty();
	}

	[Test]
	public async Task BookingReturnsAlreadyBookedStateWhenAirlineClineThrowsApiExceptionWithStatusCodeConflict() {
		_hotAirApiMock.Setup(api => api.BookFlightAsync(new BookFlightRequest("CA", "1A", "John Doe")))
			.ThrowsAsync(MockApiException.CreateApiException(HttpMethod.Post, HttpStatusCode.Conflict, "Already booked"));

		BookingResult result = await _client.BookAsync("CA", "1A", "John Doe", CancellationToken.None);

		result.BookingCode.Should().BeEmpty();
		result.State.Should().Be(BookingState.AlreadyBooked);
	}

	[Test]
	public async Task BookingReturnsErrorStateWhenAirlineClineThrowsApiException() {
		_hotAirApiMock.Setup(api => api.BookFlightAsync(new BookFlightRequest("CA", "1A", "John Doe")))
			.ThrowsAsync(MockApiException.CreateApiException(HttpMethod.Post, HttpStatusCode.BadGateway, "Error"));

		BookingResult result = await _client.BookAsync("CA", "1A", "John Doe", CancellationToken.None);

		result.BookingCode.Should().BeEmpty();
		result.State.Should().Be(BookingState.Error);
	}

	[Test]
	public async Task BookingReturnsSuccessAndBookingCode() {
		_hotAirApiMock.Setup(api => api.BookFlightAsync(new BookFlightRequest("CA", "1A", "John Doe")))
			.ReturnsAsync("12345");

		BookingResult result = await _client.BookAsync("CA", "1A", "John Doe", CancellationToken.None);

		result.BookingCode.Should().Be("12345");
		result.State.Should().Be(BookingState.Success);
	}
}
