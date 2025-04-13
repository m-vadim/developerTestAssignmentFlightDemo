using FluentValidation;
using FlySearch.AggregateApi.Features.FindFlight.Domain;

namespace FlySearch.AggregateApi.Features.FindFlight;

public sealed class FindFlightRequestValidator : AbstractValidator<FindFlightRequest> {
	private static readonly string[] PossibleSortBy = [nameof(Flight.FlightNumber)];

	public FindFlightRequestValidator(TimeProvider time) {
		RuleFor(request => request.FlightDate)
			.Must(flightDate => flightDate == default || flightDate >= time.GetLocalNow().Date)
			.WithMessage("Flight date can not be in past");

		RuleFor(request => request.SortBy)
			.Must(sb => sb == null || PossibleSortBy.Contains(sb))
			.WithMessage("You can sort by this field");
	}
}
