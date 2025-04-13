using FluentValidation;
using FlySearch.AirwaysApi.Airways.RoyalAir.BookFlight;
using FlySearch.AirwaysApi.Airways.RoyalAir.Domain;
using FlySearch.AirwaysApi.Airways.RoyalAir.FindFlight;

namespace FlySearch.AirwaysApi.Airways.RoyalAir;

public static class DependencyInjectionRegistration {
	public static IServiceCollection RegisterRoyalAir(this IServiceCollection services) {
		services.AddScoped<IValidator<FindFlightRequest>, FindFlightRequestValidator>();
		services.AddScoped<IValidator<BookFlightRequest>, BookFlightRequestValidator>();
		services.AddSingleton<IRoyalAirBooking, RoyalAirBooking>();

		return services;
	}

	public static void RegisterRoyalAirEndpoints(this WebApplication app) {
		app.AddFindFlightEndpoint();
		app.AddBookFlightEndpoint();
	}
}
