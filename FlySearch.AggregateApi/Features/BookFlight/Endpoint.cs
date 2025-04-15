using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FlySearch.AggregateApi.Features.BookFlight;

public static class Endpoint {
	public static void AddBookFlightEndpoint(this IEndpointRouteBuilder app) {
		app.MapPost("api/v1/book-flight", FindFlight)
			.WithName("Book Flight")
			.WithOpenApi()
			.WithTags("FlySearch")
			.AddEndpointFilter<RequestLoggingFilter>();
	}

	private static async Task<IResult> FindFlight(
		[FromBody] BookFlightRequest request,
		IValidator<BookFlightRequest> requestValidator,
		ICommandDispatcher dispatcher,
		CancellationToken cancellationToken) {
		var validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid) {
			return Results.ValidationProblem(validationResult.ToDictionary());
		}

		var command = new BookFlightCommand(
			request.FlightNumber,
			request.SeatNumber,
			request.Username,
			request.Airline);
		var result = await dispatcher.Dispatch<BookFlightCommand, BookingResult>(command, cancellationToken);
		if (result.State == BookingState.Success) {
			return Results.Ok(result.BookingCode);
		}

		if (result.State == BookingState.AlreadyBooked) {
			return Results.Conflict("Booking already exists");
		}

		return Results.InternalServerError("An error occurred while booking the flight. Please try again later.");
	}
}
