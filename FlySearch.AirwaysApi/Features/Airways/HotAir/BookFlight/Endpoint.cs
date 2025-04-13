using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using Microsoft.AspNetCore.Mvc;

namespace FlySearch.AirwaysApi.Airways.HotAir.BookFlight;

public static class Endpoint {
	public static void AddBookFlightEndpoint(this IEndpointRouteBuilder app) {
		app.MapPost("api/hot-air/v1/book-flight", BookFlight)
			.WithName("HotAir - Book Flight")
			.WithOpenApi()
			.WithTags("HotAir");
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
		isBooked = await commandDispatcher.Dispatch<BookFlightCommand, bool>(command, cancellationToken);
		if (!isBooked) {
			return Results.Conflict("Booking failed");
		}

		return Results.Ok();
	}
}
