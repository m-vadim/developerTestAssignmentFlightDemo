using FluentValidation;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.BookFlight;

public sealed class BookFlightRequestValidator : AbstractValidator<BookFlightRequest> {
	public BookFlightRequestValidator() {
		RuleFor(request => request.FlightNumber)
			.NotEmpty().WithMessage("Flight number is required.")
			.MaximumLength(10).WithMessage("Flight number cannot exceed 10 characters.");

		RuleFor(request => request.SeatNumber)
			.NotEmpty().WithMessage("Seat number is required.")
			.MaximumLength(5).WithMessage("Seat number cannot exceed 5 characters.")
			.Matches(@"^[0-9]+[A-Z]$").WithMessage("Seat number must be in format like '12A', '23B', etc.");

		RuleFor(request => request.Username)
			.NotEmpty().WithMessage("Username is required.")
			.MinimumLength(3).WithMessage("Username must be at least 3 characters.")
			.MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");
	}
}
