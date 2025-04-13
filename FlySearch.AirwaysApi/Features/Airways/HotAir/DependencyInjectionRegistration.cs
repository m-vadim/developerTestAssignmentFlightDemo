using FluentValidation;
using FlySearch.AirwaysApi.Airways.HotAir.BookFlight;
using FlySearch.AirwaysApi.Airways.HotAir.Domain;
using FlySearch.AirwaysApi.Airways.HotAir.FindFlight;

namespace FlySearch.AirwaysApi.Airways.HotAir;

public static class DependencyInjectionRegistration {
	public static IServiceCollection RegisterHotAir(this IServiceCollection services) {
		services.AddScoped<IValidator<FindFlightRequest>, FindFlightRequestValidator>();
		services.AddScoped<IValidator<BookFlightRequest>, BookFlightRequestValidator>();
		services.AddSingleton<IHotAirBooking, HotAirBooking>();

		return services;
	}

	public static void RegisterHotAirEndpoints(this WebApplication app) {
		app.AddFindFlightEndpoint();
		app.AddBookFlightEndpoint();
	}
}
