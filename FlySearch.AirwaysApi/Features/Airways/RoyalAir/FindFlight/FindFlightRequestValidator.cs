using FluentValidation;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.FindFlight;

public sealed class FindFlightRequestValidator : AbstractValidator<FindFlightRequest> {
	public FindFlightRequestValidator(TimeProvider time) {
		RuleFor(request => request.FlightDate)
			.Must(flightDate => flightDate == default || flightDate >= time.GetLocalNow().Date)
			.WithMessage("Flight date can not be in past");
	}
}
