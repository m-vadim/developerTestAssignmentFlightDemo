using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.FindFlight;

public static class Endpoint {
	public static void AddFindFlightEndpoint(this IEndpointRouteBuilder app) {
		app.MapGet("api/royal-air/v1/find-flight", FindFlight)
			.WithName("RoyalAir - Find Flight")
			.WithOpenApi()
			.WithTags("RoyalAir");
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

		var query = new FindFlightsQuery { FlightDate = request.FlightDate, FlightNumber = request.FlightNumber, Destination = request.Destination, Origin = request.Origin };
		FlightDescription[] data = await dispatcher.Dispatch<FindFlightsQuery, FlightDescription[]>(query, cancellationToken);

		return Results.Ok(data);
	}
}
