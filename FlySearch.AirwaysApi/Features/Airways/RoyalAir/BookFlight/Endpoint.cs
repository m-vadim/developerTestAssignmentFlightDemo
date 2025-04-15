using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using Microsoft.AspNetCore.Mvc;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.BookFlight;

public static class Endpoint {
	public static void AddBookFlightEndpoint(this IEndpointRouteBuilder app) {
		app.MapPost("api/royal-air/v1/book-flight", BookFlight)
			.WithName("RoyalAir - Book Flight")
			.WithOpenApi()
			.WithTags("RoyalAir");
	}

	private static async Task<IResult> BookFlight(
		[FromBody] BookFlightRequest request,
		IValidator<BookFlightRequest> requestValidator,
		IQueryDispatcher queryDispatcher,
		ICommandDispatcher commandDispatcher,
		CancellationToken cancellationToken) {
		var validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid) {
			return Results.ValidationProblem(validationResult.ToDictionary());
		}

		var query = new IsFlightBookedQuery(request.FlightNumber, request.SeatNumber);
		var isBooked = await queryDispatcher.Dispatch<IsFlightBookedQuery, bool>(query, cancellationToken);
		if (isBooked) {
			return Results.Conflict("Already booked");
		}

		var command = new BookFlightCommand(request.FlightNumber, request.SeatNumber, request.Username);
		string bookingCode = await commandDispatcher.Dispatch<BookFlightCommand, string>(command, cancellationToken);
		if (string.IsNullOrWhiteSpace(bookingCode)) {
			return Results.Conflict("Booking failed");
		}

		return Results.Ok(bookingCode);
	}
}
