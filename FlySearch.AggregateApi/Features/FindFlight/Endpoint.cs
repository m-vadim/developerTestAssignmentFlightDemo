using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.Features.FindFlight.Domain;

namespace FlySearch.AggregateApi.Features.FindFlight;

public static class Endpoint {
	public static void AddFindFlightEndpoint(this IEndpointRouteBuilder app) {
		app.MapGet("api/v1/find-flight", FindFlight)
			.WithName("Find Flight")
			.WithOpenApi()
			.WithTags("FlySearch");
	}

	private static async Task<IResult> FindFlight(
		[AsParameters] FindFlightRequest request,
		IValidator<FindFlightRequest> requestValidator,
		IQueryDispatcher dispatcher,
		CancellationToken cancellationToken) {
		var validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid) {
			return Results.ValidationProblem(validationResult.ToDictionary());
		}

		var query = new FindFlightsQuery {
			FlightDate = request.FlightDate,
			FlightNumber = request.FlightNumber,
			Destination = request.Destination,
			Origin = request.Origin,
			SortBy = request.SortBy
		};
		Flight[] data = await dispatcher.Dispatch<FindFlightsQuery, Flight[]>(query, cancellationToken);

		return Results.Ok(data);
	}
}
